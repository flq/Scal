using System;
using System.Windows;
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


            ViewLocation
                .ModelsMatching<DateTime>(m => m.Use((b, vm) => b.Start<DatePicker>(vm).StaticStyle("EditableDate").BindSelectedDate()))
                .ModelsMatching<CustomerNumber>(m => m.FromMatchingDataTemplate());

            StartViewModel<MainViewModel>();
        }
    }
}