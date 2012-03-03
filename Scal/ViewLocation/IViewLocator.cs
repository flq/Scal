using System.Windows;
using DynamicXaml.Extensions;

namespace Scal.ViewLocation
{
    public interface IViewLocator
    {
        Maybe<UIElement> LocateView(LocationContext locationContext);
    }
}