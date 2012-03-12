using System;
using System.Windows;
using DynamicXaml.Extensions;

namespace Scal.Services
{
    public class DataContextOverride
    {
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.RegisterAttached(
            "DataContext",
            typeof (object),
            typeof (DataContextOverride),
            new FrameworkPropertyMetadata(default(object), new PropertyChangedCallback(HandleContextSet))
            );

        private static void HandleContextSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;
            d.ToMaybeOf<FrameworkElement>().Do(fw => fw.Loaded += HandleLoaded);
        }

        private static void HandleLoaded(object sender, RoutedEventArgs e)
        {
            var fw = (FrameworkElement)sender;
            try
            {
                var ctx = GetDataContext(fw);
                fw.DataContext = ctx;
            }
            finally
            {
                fw.Loaded -= HandleLoaded;
            }
        }

        public static void SetDataContext(UIElement element, object value)
        {
            element.SetValue(DataContextProperty, value);
        }

        public static object GetDataContext(UIElement element)
        {
            return element.GetValue(DataContextProperty);
        }
    }
}