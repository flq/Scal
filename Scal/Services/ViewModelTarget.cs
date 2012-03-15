using System.Windows;

namespace Scal.Services
{
    public class ViewModelTarget
    {
        public static readonly DependencyProperty IdProperty = DependencyProperty.RegisterAttached(
            "Id",
            typeof (string),
            typeof (ViewModelTarget),
            new FrameworkPropertyMetadata(default(string))
            );

        public static void SetId(UIElement element, string value)
        {
            element.SetValue(IdProperty, value);
        }

        public static string GetId(UIElement element)
        {
            return (string) element.GetValue(IdProperty);
        }
    }
}