namespace Futures
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;

    public class TestFuture<T> : IFuture<T>
    {
        public TestFuture()
        {
            this.Observers = new List<IFutureObserver<T>>();
        }

        public List<IFutureObserver<T>> Observers { get; private set; }

        public IDisposable Subscribe(IFutureObserver<T> observer)
        {
            this.Observers.Add(observer);
            return Disposable.Create(() => this.Observers.Remove(observer));
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