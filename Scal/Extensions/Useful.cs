using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using Caliburn.Micro;
using System.Linq;

namespace Scal
{
    public static class Extensions
    {

        public static void Raise(this EventHandler @event, object sender)
        {
            if (@event != null)
                @event(sender, EventArgs.Empty);
        }

        public static void Raise<T>(this EventHandler<T> @event, object sender, T args) where T : EventArgs
        {
            if (@event != null)
                @event(sender, args);
        }

        public static void Raise(this PropertyChangedEventHandler @event, object sender, string property)
        {
            if (@event != null)
                @event(sender, new PropertyChangedEventArgs(property));
        }

        public static void Raise<T>(this PropertyChangedEventHandler @event, T sender, Expression<Func<T, object>> property)
        {
            if (@event != null)
                @event(sender, new PropertyChangedEventArgs(property.GetMemberInfo().Name));
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
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

        public static string PascalToWhitespace(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            var b = new StringBuilder();
            b.Append(input.First());
            foreach (var ch in input.Skip(1))
            {
                if (char.IsUpper(ch))
                    b.Append(" ");
                b.Append(ch);
            }
            return b.ToString();
        }
    }
}