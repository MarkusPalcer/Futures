namespace FutureTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Futures;

    public class TestObserver<T> : IFutureObserver<T>
    {
        private readonly List<Notification<T>> _events = new List<Notification<T>>();

        public TestObserver()
        {
            this.ResetEvent = new ManualResetEventSlim();
        }

        public ManualResetEventSlim ResetEvent { get; private set; }

        public IReadOnlyList<Notification<T>> Events
        {
            get
            {
                return this._events;
            }
        }

        public void OnDone(T result)
        {
            this._events.Add(Notification<T>.OnDone(result));
            this.ResetEvent.Set();
        }

        public void OnError(Exception exception)
        {
            this._events.Add(Notification<T>.OnError(exception));
            this.ResetEvent.Set();
        }
    }
}