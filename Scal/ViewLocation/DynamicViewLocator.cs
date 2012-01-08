using System.Windows;
using DynamicXaml;
using DynamicXaml.Extensions;
using Scal.Configuration;

namespace Scal.ViewLocation
{
    internal class DynamicViewLocator : IViewLocator
    {
        private readonly ModelMatchConfiguration _configuration;
        private readonly XamlBuilder _builder;

        public DynamicViewLocator(ModelMatchConfiguration configuration, XamlBuilder builder)
        {
            _configuration = configuration;
            _builder = builder;
        }

        Maybe<UIElement> IViewLocator.LocateView(object viewModel, DependencyObject visualParent, object context)
        {
            if (!_configuration.Matches(viewModel))
              return Maybe<UIElement>.None;
            return _configuration.Build(_builder, viewModel).ToMaybe();
        }
    }
}