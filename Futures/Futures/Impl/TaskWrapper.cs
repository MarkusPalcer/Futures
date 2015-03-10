namespace Futures
{
    using System;
    using System.Reactive.Disposables;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskWrapper<T> : IFuture<T>
    {
        private readonly Task<T> _task;

        internal TaskWrapper(Task<T> task)
        {
            _task = task;
        }

        public IDisposable Subscribe(IFutureObserver<T> observer)
        {
            var src = new CancellationTokenSource();
            var b = new BooleanDisposable();
            var disp = new CompositeDisposable(b, Disposable.Create(src.Cancel));

            _task.ContinueWith(
                t =>
                {
                    if (b.IsDisposed)
                    {
                        return;
                    }

                    if (t.IsFaulted)
                    {
                        var ex = t.Exception;
                        if (ex != null)
                        {
                            observer.OnError(ex.Flatten().InnerException);
                        }
                    }
                    else if (!t.IsCanceled)
                    {
                        observer.OnDone(t.Result);
                    }
                    else
                    {
                        observer.OnError(new TaskCanceledException(t));
                    }
                }, 
                src.Token);

            return disp;
        }
    }
}