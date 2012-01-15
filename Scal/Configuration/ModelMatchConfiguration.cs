using System;
using System.Windows;
using System.Windows.Controls;
using DynamicXaml;
using DynamicXaml.ResourcesSystem;
using StructureMap;
using DynamicXaml.Extensions;

namespace Scal.Configuration
{
    public class ModelMatchConfiguration
    {
        private readonly Func<object, bool> _match;
        private Func<XamlBuilder, object, dynamic> _builder;
        private bool _useADataTemplate;

        public ModelMatchConfiguration(Func<object, bool> match)
        {
            _match = match;
        }

        public void Use(Func<XamlBuilder,object,dynamic> builder)
        {
            _builder = builder;
        }

        public UIElement Build(IContainer builder, object viewModel)
        {
            if (_builder != null)
                return CreateByBuilder(builder, viewModel);
            return FromDataTemplate(builder, viewModel);
        }

        public bool Matches(object viewModel)
        {
            return _match(viewModel);
        }

        public void FromMatchingDataTemplate()
        {
            _useADataTemplate = true;
        }

        private UIElement CreateByBuilder(IContainer builder, object viewModel)
        {
            var element = _builder(builder.GetInstance<XamlBuilder>(), viewModel);
            return element.Create();
        }

        private UIElement FromDataTemplate(IContainer builder, object viewModel)
        {
            var svc = builder.GetInstance<DataTemplateService>();
            return svc.GetForObject(viewModel)
                .Get(dt => dt.LoadContent())
                .Cast<FrameworkElement>()
                .Do(fw => fw.DataContext = viewModel)
                .MustHaveValue(new TextBlock { Text = "Failed to find DataTemplate for " + viewModel.GetType().Name });
        }
    }
}