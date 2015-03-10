namespace Futures
{
    using System;
    using System.Reactive.Linq;

    public static partial class Future
    {
        public static IObservable<T> ToObservable<T>(this IFuture<T> future)
        {
            return Observable.Create<T>(observer =>
                {
                    var futureObserver = FutureObserver.Create<T>(
                        result =>
                        {
                            observer.OnNext(result);
                            observer.OnCompleted();
                        },
                observer.OnError);

                    return future.Subscribe(futureObserver);
                });
        }
    }
}