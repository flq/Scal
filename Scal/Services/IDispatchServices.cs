using System;
using System.Threading;
using System.Windows.Threading;

namespace Scal.Services
{
    public interface IDispatchServices
    {
        Dispatcher Dispatcher { get; }
        SynchronizationContext SyncContext { get; }
        void EnsureActionOnDispatcher(Action action);
        void QueueOnDispatcher(Action action);
        void Callback(TimeSpan period, Action action);
    }
}