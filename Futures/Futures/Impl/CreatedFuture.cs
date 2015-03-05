using System;
using System.Reactive.Disposables;

namespace Futures
{
    internal class CreatedFuture<T> : IFuture<T>
    {
        private readonly Func<IFutureObserver<T>, IDisposable> _subscribe;

        internal CreatedFuture(Func<IFutureObserver<T>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IFutureObserver<T> observer)
        {
            // Ensure that no further result will be returned when the subscription is cancelled
            // And that the subscription is cancelled once a result is returned.
                
            var b = new BooleanDisposable();
            var disp = new CompositeDisposable(b);

            var obs = FutureObserver.Create<T>(result =>
            {
                if (b.IsDisposed)
                {
                    return;
                }

                observer.OnDone(result);
                disp.Dispose();
            },
                ex =>
                {
                    if (b.IsDisposed)
                    {
                        return;
                    }

                    observer.OnError(ex);
                    disp.Dispose();
                });

            // If subscribing itself throws, we just pass the exception along
            try
            {
                disp.Add(_subscribe(obs));
            }
            catch (Exception ex)
            {
                obs.OnError(ex);
            }

            return disp;
        }
    }
}