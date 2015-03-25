namespace Futures
{
    using System.Reactive.Disposables;
    using System.Threading;
    using System.Threading.Tasks;

    public static partial class Future
    {
        /// <summary>
        /// Unpacks the inner <see cref="IFuture{T}"/>, removing the nested structure
        /// </summary>
        /// <typeparam name="T">The type of the inner value</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to flatten.</param>
        /// <returns>An <see cref="IFuture{T}"/> which returns as soon as the inner <see cref="IFuture{T}"/> has produced its value</returns>
        public static IFuture<T> Flatten<T>(this IFuture<IFuture<T>> source)
        {
            return Create<T>(
                observer =>
                {
                    var disp = new MultipleAssignmentDisposable();
                    disp.Disposable = source.Subscribe(innerFuture => disp.Disposable = innerFuture.Subscribe(observer), observer.OnError);
                    return disp;
                });
        }

        /// <summary>
        /// Caches the result of the specified future
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The future to cache.</param>
        /// <returns>An <see cref="IFuture{T}"/> which caches the result of the given <see cref="IFuture{T}"/>, causing all subscribers to this future to share a single subscription</returns>
        /// <remarks>This method does not subscribe to the underlying <see cref="IFuture{T}"/> until a subscription is made to the resulting <see cref="IFuture{T}"/></remarks>
        public static IFuture<T> Cache<T>(this IFuture<T> source)
        {
            var semaphore = new SemaphoreSlim(1);
            IFuture<T> tw = null;
            return Create<T>(
                o =>
                    {
                        semaphore.Wait();
                        try
                        {
                            if (tw == null)
                            {
                                var tcs = new TaskCompletionSource<T>();
                                source.Subscribe(tcs.SetResult, tcs.SetException);
                                tw = tcs.Task.ToFuture();
                            }

                            return tw.Subscribe(o);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });
        }

        /// <summary>
        /// Subscribes immediately to the given future and caches its result.
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The future to cache.</param>
        /// <returns>An <see cref="IFuture{T}"/> which returns the result of the subscription to the given <see cref="IFuture{T}"/> to all subscribers</returns>
        /// <remarks>This function will subscribe to the underlying <see cref="IFuture{T}"/>, even when no subscription is made to the resulting <see cref="IFuture{T}"/></remarks>
        public static IFuture<T> Prefetch<T>(this IFuture<T> source)
        {
            var tcs = new TaskCompletionSource<T>();
            source.Subscribe(tcs.SetResult, tcs.SetException);

            return tcs.Task.ToFuture();
        }

        /// <summary>
        /// Materializes the implicit notifications of a future as explicit notification values.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the result of the future.</typeparam>
        /// <param name="source">An future to get notification values for.</param>
        /// <returns>
        /// An future returning the materialized notification value from the source future.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IFuture<Notification<T>> Materialize<T>(this IFuture<T> source)
        {
            return Create<Notification<T>>(
                o => source.Subscribe(
                    r => o.OnDone(Notification.OnDone(r)), 
                    ex => o.OnDone(Notification.OnError<T>(ex))));
        } 
    }
}
