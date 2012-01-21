using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MemBus;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Scal.Bootstrapping
{
    /// <summary>
    /// This class registers types that are created by structuremap and whose name ends on ViewModel with the subscribing functionality of Membus.
    /// 
    /// Even though a type is registered in StructureMap, it appears that the "EnrichWith" delegate is called every time the plugin is requested.
    /// It is debatable whether this is a bug, or unexpected behavior. 
    /// 
    /// Workaround:
    /// 
    /// The Container's plugin metamodel is inspected to check whether the requested type is registered as Singleton. If it is it will be remembered
    /// in an internal HashSet. If it is a previously requested type, it will be noticed and hence the "Subscribe" procedure will not go forward.
    /// That way it should remain ensured that Singletons only get registered once in Membus.
    /// </summary>
    public class AutoSubscriberRegistrationConvention : IRegistrationConvention
    {
        private readonly AppModel _appModel;
        private static readonly HashSet<Type> singletonTypes = new HashSet<Type>();

        public AutoSubscriberRegistrationConvention(AppModel appModel)
        {
            _appModel = appModel;
        }

        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract)
                return;
            if (_appModel.RegisterHandlePredicate(type))
            {
                registry.For(type).EnrichWith((ctx, obj) =>
                {
                    var pluginType = obj.GetType();
                    if (IsSingleton(ctx.GetInstance<IContainer>(), pluginType))
                    {
                        if (singletonTypes.Contains(pluginType))
                            return obj;
                        singletonTypes.Add(pluginType);
                    }

                    Debug.WriteLine("Registering plugintype " + pluginType + " on Membus.");
                    var subs = ctx.GetInstance<ISubscriber>();
                    subs.Subscribe(obj);
                    return obj;
                });
            }
        }

        private static bool IsSingleton(IContainer structureMap, Type pluginType)
        {
            var pluginMeta = structureMap.Model.PluginTypes.FirstOrDefault(c => pluginType.Equals(c.PluginType));
            return pluginMeta != null && pluginMeta.Lifecycle.Equals("singleton", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public class HandlerRegistrationOnViewModelsConvention
    {
        
    }
}