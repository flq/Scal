using System;

namespace Scal.Configuration
{
    public class ScalConfiguration
    {
        private readonly AssemblyScanConfiguration _pool = new AssemblyScanConfiguration();
        private readonly ViewLocationConfiguration _viewLoc = new ViewLocationConfiguration();
        private Type _startViewModel;

        internal void Configure(AppModel model)
        {
            model.Assemblies = _pool.Assemblies;
            model.StartupViewModel = _startViewModel;
            model.SetViewLocators(_viewLoc.Locators);
            model.MemBusSetups = Messaging.GetSetups();
            model.RegisterHandlePredicate = Messaging.MessagingSubscriberMatcher;
            model.RegisterMessageHubPredicate = Messaging.MessageHubMatcher;
        }

        protected internal void StartViewModel<T>()
        {
            _startViewModel = typeof(T);
        }

        protected internal AssemblyScanConfiguration AssemblyPool
        {
            get { return _pool; }
        }

        protected internal ViewLocationConfiguration ViewLocation
        {
            get { return _viewLoc; }
        }

        private readonly MessagingConfiguration _messagingConfig = new MessagingConfiguration();

        protected internal MessagingConfiguration Messaging
        {
            get { return _messagingConfig; }
        }
    }
}
