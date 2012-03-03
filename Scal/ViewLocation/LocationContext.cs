using System.Windows;
using System.Windows.Media;
using DynamicXaml.Extensions;

namespace Scal.ViewLocation
{
    public class LocationContext
    {
        private readonly object _viewModel;
        private readonly DependencyObject _visualParent;
        private readonly object _context;

        public LocationContext(object viewModel, DependencyObject visualParent, object context)
        {
            _viewModel = viewModel;
            _visualParent = visualParent;
            _context = context;
        }

        public object Model { get { return _viewModel; } }

        public Maybe<object> CaliburnContext { get { return _context.ToMaybe(); } }

        public string ViewName { get { return VisualParent.ToMaybe().Cast<FrameworkElement>().Get(fe => fe.Name).Value; } }

        public DependencyObject VisualParent { get { return _visualParent; } }

        public Maybe<object> ParentDataContext {get
        {
            var dep = VisualParent;
            FrameworkElement fw;
            while (dep != null && (fw = dep as FrameworkElement) != null)
            {
                if (fw.DataContext != null && !fw.DataContext.Equals(_viewModel))
                    return fw.DataContext.ToMaybe();
                dep = VisualTreeHelper.GetParent(dep);
            }
            return Maybe<object>.None;
        }}
    }
}