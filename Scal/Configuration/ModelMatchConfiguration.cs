using System;
using System.Windows;
using System.Windows.Controls;
using DynamicXaml;
using DynamicXaml.ResourcesSystem;
using Scal.ViewLocation;
using StructureMap;
using DynamicXaml.Extensions;

namespace Scal.Configuration
{
    public class ModelMatchConfiguration
    {
        private readonly Func<LocationContext, bool> _match;
        private Func<XamlBuilder, LocationContext, dynamic> _builder;
        private bool _useADataTemplate;

        public ModelMatchConfiguration(Func<LocationContext, bool> match)
        {
            _match = match;
        }

        public void Use(Func<XamlBuilder,LocationContext,dynamic> builder)
        {
            _builder = builder;
        }

        public UIElement Build(IContainer builder, LocationContext ctx)
        {
            if (_builder != null)
                return CreateByBuilder(builder, ctx);
            return FromDataTemplate(builder, ctx);
        }

        public bool Matches(LocationContext locationContext)
        {
            return _match(locationContext);
        }

        public void FromMatchingDataTemplate()
        {
            _useADataTemplate = true;
        }

        private UIElement CreateByBuilder(IContainer builder, LocationContext ctx)
        {
            var element = _builder(builder.GetInstance<XamlBuilder>(), ctx);
            return element.Create();
        }

        private UIElement FromDataTemplate(IContainer builder, LocationContext ctx)
        {
            var svc = builder.GetInstance<DataTemplateService>();
            return svc.GetForObject(ctx.Model)
                .Get(dt => dt.LoadContent())
                .Cast<FrameworkElement>()
                .Do(fw => fw.DataContext = ctx.Model)
                .MustHaveValue(new TextBlock { Text = "Failed to find DataTemplate for " + ctx.Model.GetType().Name });
        }
    }
}