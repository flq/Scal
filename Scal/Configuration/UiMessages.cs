using System;
using MemBus;
using MemBus.Publishing;
using MemBus.Setup;
using Scal.Services;
using StructureMap;

namespace Scal.Configuration
{
    public class UiMessages : ISetup<IConfigurableBus>
    {
        private readonly Func<MessageInfo, bool> _predicate;

        public UiMessages(Func<MessageInfo, bool> predicate)
        {
            _predicate = predicate;
        }

        public void Accept(IConfigurableBus setup)
        {
            setup.ConfigurePublishing(DispatchUiMessages);
        }

        private void DispatchUiMessages(IConfigurablePublishing obj)
        {
            obj.MessageMatch(_predicate).PublishPipeline(
                new UiMsgDispatcher(ObjectFactory.GetInstance<IDispatchServices>()));
        }


        private class UiMsgDispatcher : IPublishPipelineMember
        {
            private readonly IDispatchServices _svc;
            private readonly SequentialPublisher _seq = new SequentialPublisher();

            public UiMsgDispatcher(IDispatchServices svc)
            {
                _svc = svc;
            }

            public void LookAt(PublishToken token)
            {
                _svc.EnsureActionOnDispatcher(() => _seq.LookAt(token));
            }
        }
    }
}