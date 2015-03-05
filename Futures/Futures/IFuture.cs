using System;

namespace Futures
{
    public interface IFuture<out T>
    {
        IDisposable Subscribe(IFutureObserver<T> observer);
    }
}