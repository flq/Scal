using System;
using System.Windows;

namespace Scal.Configuration
{
    public interface IExceptionHandler
    {
        bool ShouldTerminateApp(Exception x);
    }

    internal class DefaultExceptionHandler : IExceptionHandler
    {
        public bool ShouldTerminateApp(Exception x)
        {
            var result = MessageBox.Show(x.FullOutput(), "Terminate Application?", MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }
    }
}