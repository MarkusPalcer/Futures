namespace Futures
{
    using System.Reactive.Disposables;
    using System.Threading.Tasks;

    public static partial class Future
    {
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

        public static IFuture<T> Cache<T>(this IFuture<T> source)
        {
            var tcs = new TaskCompletionSource<T>();
            source.Subscribe(tcs.SetResult, tcs.SetException);

            return new TaskWrapper<T>(tcs.Task);
        } 
    }
}
