using System;
using System.Windows;

namespace SampleApp
{
    public class FunHub : IHub
    {
        public FunHub()
        {
            
        }

        public void Handle(NiceUiMsg msg)
        {
            throw new ArgumentException();
            MessageBox.Show("Got message!");
        }
    }

    public class NiceUiMsg
    {
    }

    public interface IHub
    {
    }
}