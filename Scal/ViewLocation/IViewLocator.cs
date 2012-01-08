using System.Windows;
using DynamicXaml.Extensions;

namespace Scal.ViewLocation
{
    public interface IViewLocator
    {
        Maybe<UIElement> LocateView(object viewModel, DependencyObject visualParent, object context = null);
    }
}