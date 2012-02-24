using System.Windows;
using DynamicXaml;
using DynamicXaml.Extensions;
using Scal.Configuration;
using StructureMap;

namespace Scal.ViewLocation
{
    internal class DynamicViewLocator : IViewLocator
    {
        private readonly ModelMatchConfiguration _configuration;
        private readonly IContainer _container;

        public DynamicViewLocator(ModelMatchConfiguration configuration, IContainer container)
        {
            _configuration = configuration;
            _container = container;
        }

        Maybe<UIElement> IViewLocator.LocateView(object viewModel, DependencyObject visualParent, object context)
        {
            var lCtx = new LocationContext(viewModel, visualParent, context);
            if (!_configuration.Matches(lCtx))
              return Maybe<UIElement>.None;
            return _configuration.Build(_container, lCtx).ToMaybe();
        }
    }
}