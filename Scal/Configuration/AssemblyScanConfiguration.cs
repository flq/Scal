using System.Collections.Generic;
using System.Reflection;

namespace Scal.Configuration
{
    public class AssemblyScanConfiguration
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public List<Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        public AssemblyScanConfiguration AddThisAssembly()
        {
            Assemblies.Add(Assembly.GetCallingAssembly());
            return this;
        }
    }
}