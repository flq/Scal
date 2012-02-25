using System.Windows.Input;

namespace Scal.Services
{
    public interface IGestureService
    {
        void AddKeyBinding(InputBinding binding, object scope = null);
        void RemoveInputBindings(object scope);
    }
}