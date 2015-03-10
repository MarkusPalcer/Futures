namespace Futures
{
    using System.Reactive.Disposables;
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
        public static IFuture<T> Cache<T>(this IFuture<T> source)
        {
            var tcs = new TaskCompletionSource<T>();
            source.Subscribe(tcs.SetResult, tcs.SetException);

            return new TaskWrapper<T>(tcs.Task);
        } 
    }
}
