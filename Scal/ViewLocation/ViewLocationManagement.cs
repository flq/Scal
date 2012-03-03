using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using DynamicXaml.Extensions;

namespace Scal.ViewLocation
{
    public class ViewLocationManagement
    {
        private readonly List<IViewLocator> _locators = new List<IViewLocator>();

        public void Activate()
        {
            ViewLocator.LocateForModel = Locate;
        }

        private UIElement Locate(object viewModel, DependencyObject visualParent, object context)
        {
            var lCtx = new LocationContext(viewModel, visualParent, context);
            var view = _locators.Select(l => l.LocateView(lCtx)).MaybeFirst();

            return view.MustHaveValue(ConstructFailureElement(viewModel));            
        }

        private static UIElement ConstructFailureElement(object viewModel)
        {
            return new TextBlock { Text = " Failed to produce a view for model " + viewModel };
        }

        public void Add(IEnumerable<IViewLocator> viewLocators)
        {
            if (viewLocators != null)
              _locators.AddRange(viewLocators);
        }

        internal void Add(IViewLocator viewLocator)
        {
            _locators.Add(viewLocator);
        }
    }
}