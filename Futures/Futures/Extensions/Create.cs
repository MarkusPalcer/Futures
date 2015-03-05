using System;
using System.Reactive.Disposables;

namespace Futures
{
    public static partial class Future
    {
        /// <summary>
        /// Creates a new <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="subscribe">A function that is executed for each observer that subscribes and returns an <see cref="IDisposable"/> subscription</param>
        /// <returns>A <see cref="IFuture{T}"/> that uses the given function for handling subscriptions</returns>
        public static IFuture<T> Create<T>(Func<IFutureObserver<T>, IDisposable> subscribe)
        {
            return new CreatedFuture<T>(subscribe);
        }

        /// <summary>
        /// Creates a <see cref="IFuture{T}"/> which returns the given value each time it is subscribed to.
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="value">The value to return</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns the given value each time it is subscribed to.</returns>
        public static IFuture<T> Return<T>(T value)
        {
            return Create<T>(observer =>
            {
                observer.OnDone(value);
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// Creates a <see cref="IFuture{T}"/> which fails each time it is subscribed to.
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="exception">The <see cref="Exception"/> to fail with</param>
        /// <returns>A <see cref="IFuture{T}"/> which fails with the given <see cref="Exception"/> each time it is subscribed to.</returns>
        public static IFuture<T> Fail<T>(Exception exception)
        {
            return Create<T>(observer =>
            {
                observer.OnError(exception);
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// Creates a <see cref="IFuture{T}"/> which never completes or fails.
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <returns>A <see cref="IFuture{T}"/> which never completes or fails.</returns>
        public static IFuture<T> Never<T>()
        {
            return Create<T>(observer => Disposable.Empty);
        }

        public static IDisposable Subscribe<T>(this IFuture<T> future, Action<T> onDone,
            Action<Exception> onError = null)
        {
            return future.Subscribe(FutureObserver.Create(onDone, onError ?? (_ => { })));
        } 
    }

    public static partial class FutureObserver
    {
        /// <summary>
        /// Creates a new <see cref="IFutureObserver{T}"/>
        /// </summary>
        /// <typeparam name="T">The value type of the <see cref="IFutureObserver{T}"/></typeparam>
        /// <param name="onDone">The delegate to execute when the observed <see cref="IFuture{T}"/> returns a value</param>
        /// <param name="onException">The delegate to execute when the observed <see cref="IFuture{T}"/> fails</param>
        /// <returns>A <see cref="IFutureObserver{T}"/> which calls the given delegates</returns>
        public static IFutureObserver<T> Create<T>(Action<T> onDone, Action<Exception> onException = null)
        {
            return new CreatedObserver<T>(onDone, onException ?? (_ => { }));
        }
    }
}
