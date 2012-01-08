using System;
using MemBus;

namespace Scal.Configuration
{
    public static class ScalConfigurationExtensions
    {
        /// <summary>
        /// When called, all types that are created through StructureMap and match the provided
        /// predicate will be subscribed to the Messaging bus. You can call this method several times.
        /// </summary>
        public static ScalConfiguration TypesSubscribedToMessaging(this ScalConfiguration config, Func<Type, bool> predicate)
        {
            config.AddMessagingHandlerTypes(predicate);
            return config;
        }

        public static MessagingConfiguration HandleTheseMessagesAsynchronously(this MessagingConfiguration config, Func<MessageInfo, bool> predicate)
        {
            config.AddConfigurationArtefact(new MessagingAsync(predicate));
            return config;
        }

        public static MessagingConfiguration HandleTheseMessagesOnDispatcher(this MessagingConfiguration config, Func<MessageInfo, bool> predicate)
        {
            config.AddConfigurationArtefact(new UiMessages(predicate));
            return config;
        }
    }
}