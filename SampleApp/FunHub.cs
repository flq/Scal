using System.Windows;

namespace SampleApp
{
    public class FunHub : IHub
    {
        public void Handle(NiceUiMsg msg)
        {
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