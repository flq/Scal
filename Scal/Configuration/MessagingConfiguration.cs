using System;
using System.Collections.Generic;
using MemBus.Setup;

namespace Scal.Configuration
{
    public class MessagingConfiguration
    {
        private readonly List<ISetup<IConfigurableBus>>_setups = new List<ISetup<IConfigurableBus>>();

        public void AddConfigurationArtefact(ISetup<IConfigurableBus> setup)
        {
            _setups.Add(setup);
        }

        internal IEnumerable<ISetup<IConfigurableBus>> GetSetups()
        {
            return _setups;
        }
    }
}