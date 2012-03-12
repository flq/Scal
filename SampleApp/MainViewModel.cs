using System;
using System.ComponentModel;
using MemBus;
using Scal;
using Scal.Services;

namespace SampleApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IPublisher _publisher;
        private readonly ProgramArguments _args;
        private bool _isVisible = true;
        private string _hello = "World";

        public MainViewModel(IPublisher publisher, ProgramArguments args)
        {
            _publisher = publisher;
            _args = args;
        }

        public string Hello { get { return _hello; } }

        public string TheArgs { get { return string.Join(", ", _args); } }

        public DateTime ADate { get { return DateTime.Now; } }
        public CustomerNumber Number { get { return new CustomerNumber { Number = "ABC 123"}; } }
        public MaritalStatus Status { get; set; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsVisible"));
            }
        }

        public void SendMessage()
        {
            IsVisible = !IsVisible;
            _hello += _hello;
            PropertyChanged.Raise(this, vm => vm.Hello);
            _publisher.Publish(new NiceUiMsg());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum MaritalStatus
    {
        Single,
        Married,
        Widowed,
        PolygamRelationship
    }

}