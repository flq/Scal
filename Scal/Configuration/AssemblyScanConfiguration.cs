using System.Collections.Generic;
using System.Reflection;

namespace Scal.Configuration
{
    public class AssemblyScanConfiguration
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        internal List<Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        public AssemblyScanConfiguration AddThisAssembly()
        {
            Assemblies.Add(Assembly.GetCallingAssembly());
            return this;
        }

        public AssemblyScanConfiguration AddAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }

        public AssemblyScanConfiguration AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            _assemblies.AddRange(assemblies);
            return this;
        }

        public AssemblyScanConfiguration AddAssemblies(params Assembly[] assemblies)
        {
            return AddAssemblies((IEnumerable<Assembly>)assemblies);
        }
    }
}