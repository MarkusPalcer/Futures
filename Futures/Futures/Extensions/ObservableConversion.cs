using System;
using System.Reactive.Linq;

namespace Futures
{
    public static partial class Future
    {
        public static IObservable<T> ToObservable<T>(IFuture<T> future)
        {
            return Observable.Create<T>(observer =>
            {
                var futureObserver = FutureObserver.Create<T>(result =>
                {
                    observer.OnNext(result);
                    observer.OnCompleted();
                }, observer.OnError);

                return future.Subscribe(futureObserver);
            });
        }
    }
}