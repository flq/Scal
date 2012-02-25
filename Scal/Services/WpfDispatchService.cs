using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace Scal.Services
{
    public class WpfDispatchService : IDispatchServices
    {
        private readonly Dictionary<DispatcherTimer, Action> _timers = new Dictionary<DispatcherTimer, Action>();

        public WpfDispatchService(DispatcherObject app)
        {
            Dispatcher = app.Dispatcher;
            SyncContext = new DispatcherSynchronizationContext(Dispatcher);
        }

        public Dispatcher Dispatcher { get; private set; }

        public SynchronizationContext SyncContext { get; private set; }

        public void EnsureActionOnDispatcher(Action action)
        {
            var isOnThread = Dispatcher.CheckAccess();
            if (isOnThread)
                action();
            else
                Dispatcher.Invoke(action);
        }

        public void QueueOnDispatcher(Action action)
        {
            Dispatcher.BeginInvoke(action, DispatcherPriority.Normal);
        }

        public void Callback(TimeSpan period, Action action)
        {
            var t = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher);
            t.Tick += HandleTick;
            t.Interval = period;
            _timers.Add(t, action);
            t.Start();
        }

        private void HandleTick(object sender, EventArgs e)
        {
            var t = (DispatcherTimer)sender;
            t.Stop();
            _timers[t]();
            _timers.Remove(t);
        }
    }
}