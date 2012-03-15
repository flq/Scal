using System;
using System.Collections.Generic;
using System.Linq;
using MemBus.Setup;
using MemBus.Subscribing;

namespace Scal.Configuration
{
    public class MessagingConfiguration
    {
        private readonly List<ISetup<IConfigurableBus>>_setups = new List<ISetup<IConfigurableBus>>();


        public void AddConfigurationArtefact(ISetup<IConfigurableBus> setup)
        {
            _setups.Add(setup);
        }

        internal MessagingConfiguration()
        {
            MessagingHandlerPredicates = new List<Func<Type, bool>>();
            MessageHubPredicates = new List<Func<Type, bool>>();
            SubscribeAdapterConfig = c => c.ByMethodName("Handle");
        }

        internal IEnumerable<ISetup<IConfigurableBus>> GetSetups()
        {
            var a = new FlexibleSubscribeAdapter();
            SubscribeAdapterConfig(a);
            AddConfigurationArtefact(a);
            return _setups;
        }

        internal Action<FlexibleSubscribeAdapter> SubscribeAdapterConfig { get; set; }
        internal List<Func<Type, bool>> MessagingHandlerPredicates { get; private set;}
        internal List<Func<Type, bool>> MessageHubPredicates { get; private set; }

        internal bool MessagingSubscriberMatcher(Type t)
        {
            return MessagingHandlerPredicates.Any(f => f(t));
        }

        internal bool MessageHubMatcher(Type t)
        {
            return MessageHubPredicates.Any(f => f(t));
        }
    }
}