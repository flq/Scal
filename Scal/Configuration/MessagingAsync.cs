using System;
using MemBus;
using MemBus.Publishing;
using MemBus.Setup;

namespace Scal.Configuration
{
    public class MessagingAsync : ISetup<IConfigurableBus>
    {
        private readonly Func<MessageInfo, bool> _predicate;

        public MessagingAsync(Func<MessageInfo,bool> predicate)
        {
            _predicate = predicate;
        }

        public void Accept(IConfigurableBus setup)
        {
            setup.ConfigurePublishing(AsyncMessages);
        }

        private void AsyncMessages(IConfigurablePublishing obj)
        {
            obj.MessageMatch(_predicate).PublishPipeline(new ParallelNonBlockingPublisher());
        }
    }
}