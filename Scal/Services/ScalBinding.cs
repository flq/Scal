using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Caliburn.Micro;

namespace Scal.Services
{
    public static class ServiceProviderExtension
    {
        public static IProvideValueTarget TargetProvider(this IServiceProvider provider)
        {
            return (IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget));
        }

    }

    [MarkupExtensionReturnType(typeof(Binding))]
    public class ScalBindingExtension : MarkupExtension
    {
        
        public ScalBindingExtension(string path)
        {
            Path = path;
        }

        public ScalBindingExtension() { }

        [ConstructorArgument("path")]
        public string Path { get; set; }

        public Binding KeySource { get; set; }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider.TargetProvider();

            var fw = (FrameworkElement)pvt.TargetObject;
            var bindableProperty = (DependencyProperty)pvt.TargetProperty;

            new HandleBinding(fw, bindableProperty, Path);
            return bindableProperty.DefaultMetadata.DefaultValue;
        }

        /// <summary>
        /// Why the code looks like it does:
        /// - The DataContext is not available in ProvideValue of Markupextension
        /// - Handling the ContextChange event fails when you try to set up the binding, hence usage of the load event
        /// </summary>
        private class HandleBinding
        {
            private readonly FrameworkElement _element;
            private readonly DependencyProperty _bindableProperty;
            private readonly string _path;

            public HandleBinding(FrameworkElement element, DependencyProperty bindableProperty, string path)
            {
                _element = element;
                _bindableProperty = bindableProperty;
                _path = path;
                _element.Loaded += HandleLoaded;
            }

            private void HandleLoaded(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (_element.DataContext == null) return;
                    

                    var property = _element.DataContext.GetType().GetProperty(_path);
                    var viewModelType = property.PropertyType;

                    // The code is a copy of 
                    // ConventionManager.SetBinding(viewModelType, _path, property, _element, null, _bindableProperty);
                    var b = new Binding(_path);
                    ConventionManager.ApplyBindingMode(b, property);
                    ConventionManager.ApplyValueConverter(b, _bindableProperty, property);
                    ConventionManager.ApplyStringFormat(b, null, property);
                    ConventionManager.ApplyValidation(b, viewModelType, property);
                    ConventionManager.ApplyUpdateSourceTrigger(_bindableProperty, _element, b, property);

                    BindingOperations.SetBinding(_element, _bindableProperty, b);
                }
                finally
                {
                    _element.Loaded -= HandleLoaded;
                }
            }
        }
    }
}