using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DynamicXaml.MarkupSystem;
using MemBus;

namespace Scal.Configuration
{
    public static class ScalConfigurationExtensions
    {
        /// <summary>
        /// When called, all types that are created through StructureMap and match the provided
        /// predicate will be subscribed to the Messaging bus. You can call this method several times.
        /// </summary>
        public static MessagingConfiguration TypesSubscribedToMessaging(this MessagingConfiguration config, Func<Type, bool> predicate)
        {
            config.MessagingHandlerPredicates.Add(predicate);
            return config;
        }

        /// <summary>
        /// Types that match this predicate will be instantiated during application startup and will be subscribed to the
        /// bus instance. They may serve as message hubs for e.g. handling Commands and generating responses.
        /// </summary>
        public static MessagingConfiguration TypesBeingAMessageHub(this MessagingConfiguration config, Func<Type, bool> predicate)
        {
            config.MessageHubPredicates.Add(predicate);
            return config;
        }

        /// <summary>
        /// Messages that match the predicate will be published without blocking te publisher
        /// </summary>
        public static MessagingConfiguration HandleTheseMessagesAsynchronously(this MessagingConfiguration config, Func<MessageInfo, bool> predicate)
        {
            config.AddConfigurationArtefact(new MessagingAsync(predicate));
            return config;
        }

        /// <summary>
        /// Matching messages will be ensured to be published though the Dispatcher of this application. This is useful for messages that trigger
        /// UI activity
        /// </summary>
        public static MessagingConfiguration HandleTheseMessagesOnDispatcher(this MessagingConfiguration config, Func<MessageInfo, bool> predicate)
        {
            config.AddConfigurationArtefact(new UiMessages(predicate));
            return config;
        }

        public static ConverterConfiguration ApplyDefaults(this ConverterConfiguration config)
        {
            config
                .Add<bool, Visibility, BooleanToVisibilityConverter>()
                .Add<string, ImageSource, PathToImageSourceConverter>();
            return config;
        }
    }
}