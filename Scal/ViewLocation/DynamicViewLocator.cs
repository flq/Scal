using System.Windows;
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

        Maybe<UIElement> IViewLocator.LocateView(LocationContext lCtx)
        {
            if (!_configuration.Matches(lCtx))
              return Maybe<UIElement>.None;
            return _configuration.Build(_container, lCtx).ToMaybe();
        }
    }
}