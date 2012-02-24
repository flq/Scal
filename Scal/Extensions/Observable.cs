using System;
using Scal.Services;
using System.Reactive.Linq;

namespace Scal
{
    public static class Observable
    {
        public static IObservable<T> ObserveOn<T>(this IObservable<T> observable, IDispatchServices svc)
        {
            return observable.ObserveOn(svc.SyncContext);
        }
    }
}