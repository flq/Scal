using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Scal.Configuration;
using StructureMap;
using StructureMap.TypeRules;

namespace Scal.Bootstrapping
{
    public class ScalBootstrapper : Bootstrapper
    {
        private IContainer _container;
        private readonly AppModel model = new AppModel();

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var vmType = model.StartupViewModel;

            RunStartupTasks(_container.GetAllInstances<IStartupTask>().OrderBy(st => st.Priority));
            DisplayRootViewFor(vmType);
        }


        protected override void StartRuntime()
        {
            var configType = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(t => !t.IsAbstract && t.CanBeCastTo(typeof(ScalConfiguration)));

            if (configType == null)
            {
                throw new ArgumentException("The Scal configuration has not been set. Define one by setting the ScalConfiguration property on the ScalBootstrapper");
            }

            var c = (ScalConfiguration)Activator.CreateInstance(configType);
            c.Configure(model);

            ObjectFactory.Initialize(ce =>
            {
                ce.ForSingletonOf<AppModel>().Use(model);
                ce.AddRegistry(new BootStrapRegistry(model));
            });

            _container = ObjectFactory.Container;
            base.StartRuntime();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
                service = typeof(object);
            var returnValue = key == null ? _container.GetInstance(service) : _container.GetInstance(service, key);
            return returnValue;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service).OfType<object>();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            model.HandleException(e.Exception);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return model.Assemblies;
        }

        protected override void BuildUp(object instance)
        {            
            _container.BuildUp(instance);
        }

        private void RunStartupTasks(IEnumerable<IStartupTask> tasks)
        {
            foreach (var st in tasks)
            {
                try
                {
                    st.Run();
                }
                catch (StartupTerminationException x)
                {
                    model.HandleException(x);
                    Environment.Exit(-1);
                }
                catch (Exception x)
                {
                    model.HandleException(x);
                }
            }
        }
    }
}