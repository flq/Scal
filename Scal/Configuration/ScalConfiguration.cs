using System;

namespace Scal.Configuration
{
    public class ScalConfiguration
    {
        private readonly AssemblyScanConfiguration _pool = new AssemblyScanConfiguration();
        private readonly ViewLocationConfiguration _viewLoc = new ViewLocationConfiguration();
        private readonly MessagingConfiguration _messagingConfig = new MessagingConfiguration();
        private readonly ConverterConfiguration _converterConfig = new ConverterConfiguration();
        private Type _startViewModel;
        private Type _exceptionhandler;

        internal void Configure(AppModel model)
        {
            model.Assemblies = _pool.Assemblies;
            model.StartupViewModel = _startViewModel;
            model.SetViewLocators(_viewLoc.Locators);
            model.MemBusSetups = Messaging.GetSetups();
            model.RegisterHandlePredicate = Messaging.MessagingSubscriberMatcher;
            model.RegisterMessageHubPredicate = Messaging.MessageHubMatcher;
            model.SetExceptionHandler(_exceptionhandler ?? typeof(DefaultExceptionHandler));
            model.AddConverters(_converterConfig);
        }

        protected internal void StartViewModel<T>()
        {
            _startViewModel = typeof(T);
        }

        protected internal void UnhandledExceptionsPassedTo<T>() where T : IExceptionHandler
        {
            _exceptionhandler = typeof(T);
        }

        protected internal AssemblyScanConfiguration AssemblyPool
        {
            get { return _pool; }
        }

        protected internal ViewLocationConfiguration ViewLocation
        {
            get { return _viewLoc; }
        }

        protected internal MessagingConfiguration Messaging
        {
            get { return _messagingConfig; }
        }

        protected internal ConverterConfiguration Converters
        {
            get { return _converterConfig; }
        }
    }
}
