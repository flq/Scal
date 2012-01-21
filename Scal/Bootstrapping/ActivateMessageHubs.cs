using System.Linq;
using MemBus;
using StructureMap;
using DynamicXaml.Extensions;

namespace Scal.Bootstrapping
{
    public class ActivateMessageHubs : IStartupTask
    {
        private readonly TypeCollector _collector;
        private readonly IContainer _container;
        private readonly ISubscriber _subscriber;

        public ActivateMessageHubs(TypeCollector collector, IContainer container, ISubscriber subscriber)
        {
            _collector = collector;
            _container = container;
            _subscriber = subscriber;
        }

        public void Run()
        {
            _collector
                .Select(t => _container.GetInstance(t))
                .ForEach(obj => _subscriber.Subscribe(obj));
        }

        public TaskPriority Priority
        {
            get { return TaskPriority.Earlier; }
        }
    }
}