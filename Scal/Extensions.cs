using System;
using System.Text;

namespace Scal
{
    public static class Extensions
    {
        public static void Raise<T>(this EventHandler<T> @event, object sender, T args) where T : EventArgs
        {
            if (@event != null)
                @event(sender, args);
        }

        public static string FullOutput(this Exception x)
        {
            var sb = new StringBuilder();

            while (x != null)
            {
                sb.AppendLine(string.Format("{0}: {1}", x.GetType().Name, x.Message));
                sb.AppendLine("-");
                sb.AppendLine(x.StackTrace);
                sb.AppendLine("-------------");
                x = x.InnerException;
            }
            return sb.ToString();
        } 
    }
}