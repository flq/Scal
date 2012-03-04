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

        private class HandleBinding
        {
            private readonly FrameworkElement _target;
            private readonly DependencyProperty _bindableProperty;
            private readonly string _path;

            public HandleBinding(FrameworkElement target, DependencyProperty bindableProperty, string path)
            {
                _target = target;
                _bindableProperty = bindableProperty;
                _path = path;
                _target.DataContextChanged += HandleCtxChange;
            }

            private void HandleCtxChange(object sender, DependencyPropertyChangedEventArgs e)
            {
                try
                {
                    if (e.NewValue == null)
                        return;
                    var property = e.NewValue.GetType().GetProperty(_path);
                    var viewModelType = property.PropertyType;
                    
                    // CM right now doesn't use anything from the convention
                    // which is why we pass null.
                    ConventionManager.SetBinding(viewModelType, _path, property, _target, null, _bindableProperty);
                }
                finally
                {
                    _target.DataContextChanged -= HandleCtxChange;
                }
            }
        }
    }
}