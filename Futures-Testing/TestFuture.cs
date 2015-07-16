namespace Futures
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;

    public class TestFuture<T> : IFuture<T>
    {
        private readonly IFuture<T> _impl;

        public TestFuture()
        {
            this.Observers = new List<IFutureObserver<T>>();
            _impl = Future.Create<T>(o =>
            {
                this.Observers.Add(o);
                return Disposable.Create(() => this.Observers.Remove(o));
            });
        }

        public List<IFutureObserver<T>> Observers { get; private set; }

        public IDisposable Subscribe(IFutureObserver<T> observer)
        {
            return _impl.Subscribe(observer);
        }

        public void SetResult(T result)
        {
            Array.ForEach(this.Observers.ToArray(), o => o.OnDone(result));
        }

        public void SetError(Exception error)
        {
            Array.ForEach(this.Observers.ToArray(), o => o.OnError(error));
        }
    }
}