using System;

namespace Futures
{
    internal class CreatedObserver<T> : IFutureObserver<T>
    {
        private readonly Action<T> _onDone;
        private readonly Action<Exception> _onError;

        internal CreatedObserver(Action<T> onDone, Action<Exception> onError)
        {
            _onDone = onDone;
            _onError = onError;
        }

        public void OnDone(T result)
        {
            _onDone(result);
        }

        public void OnError(Exception exception)
        {
            _onError(exception);
        }
    }
}