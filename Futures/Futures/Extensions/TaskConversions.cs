using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;

namespace Futures
{
    public static partial class Future
    {

        public static IFuture<T> FromTask<T>(Task<T> task)
        {
            return new TaskWrapper<T>(task);
        }

        /// <summary>
        /// Converts a (most likely running) <see cref="Task"/> into an <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="task">The task to convert</param>
        /// <returns>A future that calls <see cref="IFuture{T}#OnDone"/> when the task completes and <see cref="IFuture{T}#OnError"/> when it fails</returns>
        /// <remarks>
        /// The result of the task is cached, so when an <see cref="IFutureObserver{T}"/> subscribes and the task is already finished, it will get the result or error immediately.
        /// </remarks>
        public static IFuture<T> ToFuture<T>(this Task<T> task)
        {
            return FromTask(task);
        }

        /// <summary>
        /// Converts an asynchronous function into a <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="taskFactory">
        /// The task factory which is used to create a new Task each time the <see cref="IFuture{T}"/> is subscribed to.
        /// </param>
        /// <returns>A future that spawns a new task for each <see cref="IFutureObserver{T}"/> that subscribes to it</returns>
        public static IFuture<T> ToFuture<T>(this Func<Task<T>> taskFactory)
        {
            return Create<T>(observer => new TaskWrapper<T>(taskFactory()).Subscribe(observer));
        }

        /// <summary>
        /// Converts an asynchronous function into a <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="taskFactory">
        /// The task factory which is used to create a new cancellable Task each time the <see cref="IFuture{T}"/> is subscribed to.
        /// </param>
        /// <returns>A future that spawns a new task for each <see cref="IFutureObserver{T}"/> that subscribes to it</returns>
        /// <remarks>
        /// This overload causes the spawned Task to be cancelled each time the observer unsubscribes.
        /// </remarks>
        public static IFuture<T> ToFuture<T>(this Func<CancellationToken, Task<T>> taskFactory)
        {
            return Create<T>(observer =>
            {
                var source = new CancellationTokenSource();
                var disp = new CancellationDisposable(source);
                var subscription = new TaskWrapper<T>(taskFactory(source.Token)).Subscribe(observer);

                return new CompositeDisposable(disp, subscription);
            });
        }
    }
}