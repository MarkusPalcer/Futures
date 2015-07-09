namespace Futures
{
    using System;
    using System.Reactive;
    using System.Reactive.Disposables;

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

        /// <summary>
        /// Converts the action to an <see cref="IFuture{T}"/> which is executed synchronously every time it is subscribed to
        /// </summary>
        /// <param name="source">The action to execute for each subscriber</param>
        /// <returns>An <see cref="IFuture{T}"/> which executes the action synchronously each time it is subscribed to and yields <see cref="Unit#Default"/> when it is done</returns>
        public static IFuture<Unit> ToFuture(this Action source)
        {
            return Return(Unit.Default).Then(source);
        }

        /// <summary>
        /// Converts the function to an <see cref="IFuture{T}"/> which is executed synchronously every time it is subscribed to
        /// </summary>
        /// <typeparam name="T">
        /// The result type of the function
        /// </typeparam>
        /// <param name="source">
        /// The function to execute for each subscriber
        /// </param>
        /// <returns>
        /// An <see cref="IFuture{T}"/> which executes the action synchronously each time it is subscribed to and yields its result when it is done
        /// </returns>
        public static IFuture<T> ToFuture<T>(this Func<T> source)
        {
            return Return(Unit.Default).Then(source);
        }

        /// <summary>
        /// Converts the instance of <see cref="Lazy{T}"/> to an <see cref="IFuture{T}"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The result type of the function
        /// </typeparam>
        /// <param name="source">
        /// The lazy data source
        /// </param>
        /// <returns>
        /// An <see cref="IFuture{T}"/> which evaluates the Value property of the lazy source
        /// </returns>
        /// <remarks>
        /// The function that creates the value is only executed once no matter how many subscribers are added.
        /// </remarks>
        public static IFuture<T> ToFuture<T>(this Lazy<T> source)
        {
            return Create<T>(o =>
            {
                try
                {
                    o.OnDone(source.Value);
                }
                catch (Exception ex)
                {
                    o.OnError(ex);
                }

                return Disposable.Empty;
            });
        }

        /// <summary>
        /// Subscribes to the specified <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="future">The <see cref="IFuture{T}"/> to subscribe to.</param>
        /// <param name="onDone">The action to execute when the <see cref="IFuture{T}"/> returns with a result</param>
        /// <param name="onError">The action to execute when the <see cref="IFuture{T}"/> returns with an error</param>
        /// <returns>A <see cref="IDisposable"/> which when disposed cancels the subscription</returns>
        public static IDisposable Subscribe<T>(
            this IFuture<T> future,
            Action<T> onDone,
            Action<Exception> onError = null)
        {
            return future.Subscribe(FutureObserver.Create(onDone, onError ?? (_ => { })));
        } 
    }

    public static class FutureObserver
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
