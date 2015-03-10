namespace FutureTests
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;

    using Futures;

    public class TestFuture<T> : IFuture<T>
    {
        public TestFuture()
        {
            Observers = new List<IFutureObserver<T>>();
        }

        public List<IFutureObserver<T>> Observers { get; private set; }

        public IDisposable Subscribe(IFutureObserver<T> observer)
        {
            Observers.Add(observer);
            return Disposable.Create(() => Observers.Remove(observer));
        }

        public void SetResult(T result)
        {
            Array.ForEach(Observers.ToArray(), o => o.OnDone(result));
        }

        public void SetError(Exception error)
        {
            Array.ForEach(Observers.ToArray(), o => o.OnError(error));
        }
    }
}