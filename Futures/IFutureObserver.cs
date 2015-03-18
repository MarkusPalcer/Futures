using System;

namespace Futures
{
    public interface IFutureObserver<in T>
    {
        void OnDone(T result);

        void OnError(Exception exception);
    }
}