namespace Futures
{
    using System.Reactive.Disposables;

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
    }
}
