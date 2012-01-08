using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DynamicXaml.Extensions;
using Scal;
using Scal.ViewLocation;

namespace SampleApp
{
    public class NightTimeSwitch : IViewLocator
    {
        public Maybe<UIElement> LocateView(object viewModel, DependencyObject visualParent, object context = null)
        {
            return (new Rectangle { Fill = new SolidColorBrush(Colors.Black) }).ToMaybe<UIElement>();
        }
    }
}