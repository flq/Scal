using System;
using System.Windows;
using DynamicXaml;

namespace Scal.Configuration
{
    public class ModelMatchConfiguration
    {
        private readonly Func<object, bool> _match;
        private Func<XamlBuilder, object, dynamic> _builder;

        public ModelMatchConfiguration(Func<object, bool> match)
        {
            _match = match;
        }

        public void Use(Func<XamlBuilder,object,dynamic> builder)
        {
            _builder = builder;
        }

        public UIElement Build(XamlBuilder builder, object viewModel)
        {
            var element = _builder(builder, viewModel);
            return element.Create();
        }

        public bool Matches(object viewModel)
        {
            return _match(viewModel);
        }
    }
}