namespace Futures
{
    using System;
    using System.Reactive.Linq;

    public static partial class Future
    {
        /// <summary>
        /// Converts the <see cref="IFuture{T}"/> to an <see cref="IObservable{T}"/> which returns exactly one item and then completes.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="future">The future to convert</param>
        /// <returns>An <see cref="IObservable{T}"/> which returns the result of the future and then terminates.</returns>
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

        /// <summary>
        /// Unboxes the value from the futures in the given observable
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="source">The observable sequence to unbox the futures from</param>
        /// <returns>An observable sequence which contains the result of the futures in the order that they return their results</returns>
        public static IObservable<T> Unbox<T>(this IObservable<IFuture<T>> source)
        {
            return source.SelectMany(ToObservable);
        }
    }
}