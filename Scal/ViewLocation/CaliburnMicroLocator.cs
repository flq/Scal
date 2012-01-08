using System;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using DynamicXaml.Extensions;

namespace Scal.ViewLocation
{
    public class CaliburnMicroLocator : IViewLocator
    {
        private readonly Func<object, DependencyObject, object, UIElement> _original;

        public CaliburnMicroLocator()
        {
            _original = ViewLocator.LocateForModel;
        }


        public Maybe<UIElement> LocateView(object viewModel, DependencyObject visualParent, object context = null)
        {
            var ui = _original(viewModel, visualParent, context);
            if (ui is TextBlock && ((TextBlock)ui).Text.StartsWith("Cannot find view for"))
                return Maybe<UIElement>.None;
            return _original(viewModel, visualParent, context).ToMaybe();
        }

    }
}