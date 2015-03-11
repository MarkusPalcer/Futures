namespace Futures
{
    using System;
    using System.Reactive;
    using System.Reactive.Disposables;
    using System.Threading;
    using System.Threading.Tasks;

    public static partial class Future
    {
        /// <summary>
        /// Creates a new future from the specified task
        /// </summary>
        /// <typeparam name="T">The result type of the task</typeparam>
        /// <param name="task">The task to convert</param>
        /// <returns>A future which yields the result of the task once it is finished or an exception if it fails.</returns>
        /// <remarks>If the task is canceled while this future is subscribed to, a <see cref="TaskCanceledException"/> is yielded.</remarks>
        public static IFuture<T> FromTask<T>(Task<T> task)
        {
            return Create<T>(
                o =>
                    {
                        var src = new CancellationTokenSource();
                        var b = new BooleanDisposable();
                        var disp = new CompositeDisposable(b, Disposable.Create(src.Cancel));

                        task.ContinueWith(
                            t =>
                                {
                                    if (b.IsDisposed)
                                    {
                                        return;
                                    }

                                    if (t.IsFaulted)
                                    {
                                        var ex = t.Exception;
                                        if (ex != null)
                                        {
                                            o.OnError(ex.Flatten().InnerException);
                                        }
                                    }
                                    else if (!t.IsCanceled)
                                    {
                                        o.OnDone(t.Result);
                                    }
                                    else
                                    {
                                        o.OnError(new TaskCanceledException(t));
                                    }
                                },
                            src.Token);

                        return disp;
                    });
        }

        /// <summary>
        /// Creates a new future from the specified task
        /// </summary>
        /// <param name="task">The task to convert</param>
        /// <returns>A future which yields <see cref="Unit#Default"/> once it is finished or an exception if it fails.</returns>
        /// <remarks>If the task is canceled while this future is subscribed to, a <see cref="TaskCanceledException"/> is yielded.</remarks>
        public static IFuture<Unit> FromTask(Task task)
        {
            return Create<Unit>(
                o =>
                    {
                        var src = new CancellationTokenSource();
                        var b = new BooleanDisposable();
                        var disp = new CompositeDisposable(b, Disposable.Create(src.Cancel));

                        task.ContinueWith(
                            t =>
                                {
                                    if (b.IsDisposed)
                                    {
                                        return;
                                    }

                                    if (t.IsFaulted)
                                    {
                                        var ex = t.Exception;
                                        if (ex != null)
                                        {
                                            o.OnError(ex.Flatten().InnerException);
                                        }
                                    }
                                    else if (!t.IsCanceled)
                                    {
                                        o.OnDone(Unit.Default);
                                    }
                                    else
                                    {
                                        o.OnError(new TaskCanceledException(t));
                                    }
                                },
                            src.Token);

                        return disp;
                    });
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
        /// Converts a (most likely running) <see cref="Task"/> into an <see cref="IFuture{T}"/>
        /// </summary>
        /// <param name="task">The task to convert</param>
        /// <returns>A future that calls <see cref="IFuture{T}#OnDone"/> when the task completes and <see cref="IFuture{T}#OnError"/> when it fails</returns>
        /// <remarks>
        /// The result of the task is cached, so when an <see cref="IFutureObserver{T}"/> subscribes and the task is already finished, it will get the result or error immediately.
        /// </remarks>
        public static IFuture<Unit> ToFuture(this Task task)
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
            return Create<T>(observer => taskFactory().ToFuture().Subscribe(observer));
        }

        /// <summary>
        /// Converts an asynchronous function into a <see cref="IFuture{T}"/>
        /// </summary>
        /// <param name="taskFactory">
        /// The task factory which is used to create a new Task each time the <see cref="IFuture{T}"/> is subscribed to.
        /// </param>
        /// <returns>A future that spawns a new task for each <see cref="IFutureObserver{T}"/> that subscribes to it</returns>
        public static IFuture<Unit> ToFuture(this Func<Task> taskFactory)
        {
            return Create<Unit>(observer => taskFactory().ToFuture().Subscribe(observer));
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
                var subscription = taskFactory(source.Token).ToFuture().Subscribe(observer);

                return new CompositeDisposable(disp, subscription);
            });
        }

        /// <summary>
        /// Converts an asynchronous function into a <see cref="IFuture{T}"/>
        /// </summary>
        /// <param name="taskFactory">
        /// The task factory which is used to create a new cancellable Task each time the <see cref="IFuture{T}"/> is subscribed to.
        /// </param>
        /// <returns>A future that spawns a new task for each <see cref="IFutureObserver{T}"/> that subscribes to it</returns>
        /// <remarks>
        /// This overload causes the spawned Task to be cancelled each time the observer unsubscribes.
        /// </remarks>
        public static IFuture<Unit> ToFuture(this Func<CancellationToken, Task> taskFactory)
        {
            return Create<Unit>(observer =>
            {
                var source = new CancellationTokenSource();
                var disp = new CancellationDisposable(source);
                var subscription = taskFactory(source.Token).ToFuture().Subscribe(observer);

                return new CompositeDisposable(disp, subscription);
            });
        }
    }
}