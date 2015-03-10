namespace FutureTests
{
    using System;
    using System.Collections.Generic;

    using Futures;

    public class TestObserver<T> : IFutureObserver<T>
    {
        private readonly List<Notification<T>> _events = new List<Notification<T>>();

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
        }

        public void OnError(Exception exception)
        {
            this._events.Add(Notification<T>.OnError(exception));
        }
    }
}