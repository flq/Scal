using Caliburn.Micro;

namespace Scal.Services
{
    public class ScalWindowManager : WindowManager
    {
        protected override System.Windows.Window CreateWindow(object rootModel, bool isDialog, object context, System.Collections.Generic.IDictionary<string, object> settings)
        {
            return base.CreateWindow(rootModel, isDialog, context, settings);
        }
    }
}