using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DynamicXaml.Extensions;
using MemBus.Setup;
using Scal.Configuration;
using Scal.ViewLocation;
using StructureMap;

namespace Scal
{
    public class AppModel
    {
        public Type StartupViewModel { get; internal set; }

        private IEnumerable<Assembly> _assemblies;
        private readonly List<object> _viewLocators = new List<object>();

        public IEnumerable<ISetup<IConfigurableBus>> MemBusSetups { get; internal set; }

        public IEnumerable<Assembly> Assemblies
        {
            get { return (new [] { typeof(AppModel).Assembly }).Concat(_assemblies); }
            internal set { _assemblies = value; }
        }

        internal Func<Type, bool> RegisterHandlePredicate { get; set; }
        internal Func<Type, bool> RegisterMessageHubPredicate { get; set; }
        internal Type ExceptionHandler { get; private set; }
        internal IEnumerable<Tuple<Type, Type, IValueConverter>> Converters { get; private set; }

        internal IEnumerable<IViewLocator> GetViewLocators(IContainer container)
        {
            return _viewLocators.Select(o => o.Maybe(
                  v => v.Cast<IViewLocator>(),
                  v => v.Cast<Type>().Get(container.GetInstance).Cast<IViewLocator>(),
                  v => v.Cast<ModelMatchConfiguration>().Get(m => (IViewLocator)container.With(m).GetInstance<DynamicViewLocator>())
                ).MustHaveValue());
        }

        internal void SetViewLocators(IEnumerable<object> locators)
        {
            _viewLocators.AddRange(locators);
        }

        internal void SetExceptionHandler(Type exceptionHandlerType)
        {
            ExceptionHandler = exceptionHandlerType;
        }

        internal void AddConverters(IEnumerable<Tuple<Type, Type, IValueConverter>> converters)
        {
            Converters = converters;
        }
    }
}