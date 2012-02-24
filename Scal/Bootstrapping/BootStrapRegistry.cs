using System;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using DynamicXaml;
using DynamicXaml.ResourcesSystem;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using Scal.Services;
using Scal.ViewLocation;
using StructureMap.Configuration.DSL;
using DynamicXaml.Extensions;
using System.Linq;

namespace Scal.Bootstrapping
{
    public class BootStrapRegistry : Registry
    {
        public BootStrapRegistry(AppModel model)
        {
            ForSingletonOf<IWindowManager>().Use(new WindowManager());
            ForSingletonOf<ViewLocationManagement>().Use<ViewLocationManagement>();
            ForSingletonOf<ResourceService>().Use(new ResourceService(new CompositeResourceLoader(model.Assemblies.ToArray())));
            ForSingletonOf<XamlBuilder>().Use(ctx=>
            {
                var b = new XamlBuilder();
                b.GetResourcesFrom(ctx.GetInstance<ResourceService>());
                return b;
            });

            ForSingletonOf<DataTemplateService>().Use(ctx => new DataTemplateService(ctx.GetInstance<ResourceService>()));

            For<Application>().Use(Application.Current);
            Forward<Application, DispatcherObject>();
            ForSingletonOf<IDispatchServices>().Use<WpfDispatchService>();

            ForSingletonOf<IBus>().Use(()=>ConstructBus(model));
            For(typeof(IObservable<>)).Use(typeof(MessageObservable<>));
            Forward<IBus, IPublisher>();
            Forward<IBus, ISubscriber>();

            Scan(s =>
                     {
                         model.Assemblies.ForEach(s.Assembly);
                         s.LookForRegistries();
                         s.SingleImplementationsOfInterface();
                         s.With(new AutoSubscriberRegistrationConvention(model));
                         s.With(new MessageHubRegistrationConvention(model));
                         s.AddAllTypesOf<IStartupTask>();
                         s.AddAllTypesOf<IShutdownTask>();
                     });
        }

        private static IBus ConstructBus(AppModel model)
        {
            var setup =
                BusSetup.StartWith<Conservative>()
                .Apply<FlexibleSubscribeAdapter>(c => c.ByMethodName("Handle"));
            setup.Apply(model.MemBusSetups.ToArray());
            return setup.Construct();
        }
    }
}