using System;
using MemBus;

namespace SampleApp
{
    public class MainViewModel
    {
        private readonly IPublisher _publisher;

        public MainViewModel(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public string Hello { get { return "World"; } }

        public DateTime ADate { get { return DateTime.Now; } }
        public CustomerNumber Number { get { return new CustomerNumber() { Number = "ABC 123"}; } }

        public void SendMessage()
        {
            _publisher.Publish(new NiceUiMsg());
        }
    }
}