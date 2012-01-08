using DynamicXaml;
using System.Linq;

namespace Scal.Bootstrapping
{
    public class ActivateDynamicXamlFactory : IStartupTask
    {
        private readonly XamlBuilder _builder;
        private readonly AppModel _model;

        public ActivateDynamicXamlFactory(XamlBuilder builder, AppModel model)
        {
            _builder = builder;
            _model = model;
        }

        public void Run()
        {
            _builder.GetResourcesFrom(_model.Assemblies.ToArray());
        }

        public TaskPriority Priority { get { return TaskPriority.Earliest; } }
    }
}