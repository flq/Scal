using System;
using System.Windows.Controls;
using Scal;
using Scal.Configuration;
using DynamicXaml.Extensions;

namespace SampleApp
{
    public class AppConfiguration : ScalConfiguration
    {
        public AppConfiguration()
        {
            AssemblyPool.AddThisAssembly();

            Messaging
                .HandleTheseMessagesAsynchronously(mi => mi.Name.EndsWith("TaskMsg"))
                .HandleTheseMessagesOnDispatcher(mi => mi.Name.EndsWith("UiMsg"))
                .TypesSubscribedToMessaging(t => typeof(AbstractViewModel).IsAssignableFrom(t))
                .TypesBeingAMessageHub(t => t.CanBeCastTo<IHub>());

            Converters.ApplyDefaults();

            ViewLocation
                .ModelsMatching<DateTime>(m => m.Use((b, ctx) => b.Start<DatePicker>(ctx.Model).StaticStyle("EditableDate").BindSelectedDate()))
                .ModelsMatching<CustomerNumber>(m => m.FromMatchingDataTemplate());

            StartViewModel<MainViewModel>();
        }
    }
}