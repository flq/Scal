using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Scal.Services
{
    public class GestureService : IGestureService
    {
        private UIElement _view;
        private readonly Dictionary<object, List<InputBinding>> _bindings = new Dictionary<object, List<InputBinding>>();
        private readonly object _globalScope = new object();

        void IGestureService.AddKeyBinding(InputBinding binding, object scope)
        {
            var bindings = GetBindingScope(scope);
            bindings.Add(binding);
            _view.InputBindings.Add(binding);
        }

        void IGestureService.RemoveInputBindings(object scope)
        {
            var bindings = GetBindingScope(scope);
            bindings.ForEach(ib => _view.InputBindings.Remove(ib));
            _bindings.Remove(scope);
        }

        private List<InputBinding> GetBindingScope(object scope)
        {
            if (scope == null)
                scope = _globalScope;
            if (!_bindings.ContainsKey(scope))
                _bindings.Add(scope, new List<InputBinding>());
            return _bindings[scope];
        }

        public void AttachView(UIElement view)
        {
            _view = view;
        }
    }
}