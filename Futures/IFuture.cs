namespace Futures
{
    using System;

    public interface IFuture<out T>
    {
        IDisposable Subscribe(IFutureObserver<T> observer);
    }
}