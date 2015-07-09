namespace Futures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;

    public static partial class Future
    {
        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <typeparam name="TEx">The exception type to recover from</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning a valid value once done so</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<TEx, T> handler) where TEx : Exception
        {
            return Create<T>(
                o =>
                    {
                        Action<Exception> handle = ex =>
                            {
                                var e = ex as TEx;
                                if (e != null)
                                {
                                    try
                                    {
                                        o.OnDone(handler(e));
                                    }
                                    catch (Exception ex2)
                                    {
                                        o.OnError(ex2);
                                    }
                                }
                                else
                                {
                                    o.OnError(ex);
                                }
                            };

                        return source.Subscribe(o.OnDone, handle);
                    });
        }

        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <typeparam name="TEx">The exception type to recover from</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning a valid value once done so</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<T> handler) where TEx : Exception
        {
            return source.Recover<T, TEx>(_ => handler());
        }

        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <typeparam name="TEx">The exception type to recover from</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning an <see cref="IFuture{T}"/> which will yield the recovery value</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<TEx, IFuture<T>> handler)
            where TEx : Exception
        {
            return Create<T>(
                o =>
                    {
                        var disp = new MultipleAssignmentDisposable();
                        Action<Exception> handle = ex =>
                            {
                                var e = ex as TEx;
                                if (e != null)
                                {
                                    try
                                    {
                                        disp.Disposable = handler(e).Subscribe(o);
                                    }
                                    catch (Exception ex2)
                                    {
                                        o.OnError(ex2);
                                    }
                                }
                                else
                                {
                                    o.OnError(ex);
                                }
                            };

                        disp.Disposable = source.Subscribe(o.OnDone, handle);
                        return disp;
                    });
        }

        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <typeparam name="TEx">The exception type to recover from</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning an <see cref="IFuture{T}"/> which will yield the recovery value</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<IFuture<T>> handler) where TEx : Exception
        {
            return source.Recover<T, TEx>(_ => handler());
        }

        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <typeparam name="TEx">The exception type to recover from</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="recovery">A <see cref="IFuture{T}"/> which is used to recover from the error</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, IFuture<T> recovery) where TEx : Exception
        {
            return source.Recover<T, TEx>(() => recovery);
        }

        /// <summary>
        /// Recovers from all errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning a valid value once done so</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T>(this IFuture<T> source, Func<Exception, T> handler)
        {
            return Recover<T, Exception>(source, handler);
        }

        /// <summary>
        /// Recovers from all errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning a valid value once done so</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T>(this IFuture<T> source, Func<T> handler) 
        {
            return source.Recover<T, Exception>(_ => handler());
        }

        /// <summary>
        /// Recovers from all errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning an <see cref="IFuture{T}"/> which will yield the recovery value</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T>(this IFuture<T> source, Func<Exception, IFuture<T>> handler)
        {
            return Create<T>(
                o =>
                {
                    var disp = new MultipleAssignmentDisposable();
                    Action<Exception> handle = ex =>
                    {
                        if (ex != null)
                        {
                            try
                            {
                                disp.Disposable = handler(ex).Subscribe(o);
                            }
                            catch (Exception ex2)
                            {
                                o.OnError(ex2);
                            }
                        }
                        else
                        {
                            o.OnError(ex);
                        }
                    };

                    disp.Disposable = source.Subscribe(o.OnDone, handle);
                    return disp;
                });
        }

        /// <summary>
        /// Recovers from errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="handler">A function which tries to recover from the exception, returning an <see cref="IFuture{T}"/> which will yield the recovery value</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T>(this IFuture<T> source, Func<IFuture<T>> handler)
        {
            return source.Recover<T, Exception>(_ => handler());
        }

        /// <summary>
        /// Recovers from all errors
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="recovery">A <see cref="IFuture{T}"/> which is used to recover from the error</param>
        /// <returns>A <see cref="IFuture{T}"/> which only calls <see cref="IFutureObserver{T}#OnError"/> when recovery fails or the exception is not assignable to the given type</returns>
        /// <remarks>If the handler throws or the new <see cref="IFuture{T}"/> returns with an exception, the resulting future will report it to subscribers</remarks>
        public static IFuture<T> Recover<T>(this IFuture<T> source, IFuture<T> recovery)
        {
            return source.Recover<T, Exception>(() => recovery);
        }


        /// <summary>
        /// Retries the operation by subscribing to the specified <see cref="IFuture{T}"/> again after varying delays
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="delays">An enumeration of delays to wait for</param>
        /// <param name="scheduler">A scheduler to wait and retry on</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns a result as soon as one of the retries succeeds and fails with the error of the first try once all retries failed.</returns>
        /// <remarks>
        /// This function will try once for each item in <code>delays</code>, waiting for the specified delay before doing so.
        /// If the <code>delays</code> contains no more items, the original error is returned.
        /// </remarks>
        public static IFuture<T> Retry<T>(this IFuture<T> source, IEnumerable<TimeSpan> delays, IScheduler scheduler = null)
        {
            scheduler = scheduler ?? Scheduler.Default;

            return Create<T>(
                o =>
                    {
                        var retryQueue = new Queue<TimeSpan>(delays);
                        var b = new BooleanDisposable();
                        var m = new MultipleAssignmentDisposable();

                        Action<Exception> handle = null;
                        handle = ex =>
                            {
                                if (retryQueue.Count == 0)
                                {
                                    o.OnError(ex);
                                }
                                else
                                {
                                    if (b.IsDisposed)
                                    {
                                        return;
                                    }

                                    m.Disposable.Dispose();
                                    m.Disposable = scheduler.Schedule(
                                        retryQueue.Dequeue(),
                                        () =>
                                            {
                                                m.Disposable = source.Subscribe(o.OnDone, _ => handle(ex));
                                            });
                                }
                            };

                        m.Disposable = source.Subscribe(o.OnDone, handle);

                        return new CompositeDisposable(b, m);
                    });
        }

        /// <summary>
        /// Retries the operation by subscribing to the specified <see cref="IFuture{T}"/> again after a delay
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="delay">The delay to wait before retrying</param>
        /// <param name="count">The maximum number of retries</param>
        /// <param name="scheduler">A scheduler to wait and retry on</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns a result as soon as one of the retries succeeds and fails with the error of the first try once all retries failed.</returns>
        public static IFuture<T> Retry<T>(this IFuture<T> source, TimeSpan delay, int count, IScheduler scheduler = null)
        {
            return source.Retry(Enumerable.Repeat(delay, count), scheduler);
        }

        /// <summary>
        /// Retries the operation by subscribing to the specified <see cref="IFuture{T}"/> again after a delay
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="delay">The delay to wait before retrying</param>
        /// <param name="scheduler">A scheduler to wait and retry on</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns a result as soon as the retry succeeds and fails with the error of the first try once the retry fails.</returns>
        public static IFuture<T> Retry<T>(this IFuture<T> source, TimeSpan delay, IScheduler scheduler = null)
        {
            return source.Retry(new[] { delay }, scheduler);
        }

        /// <summary>
        /// Retries the operation by subscribing to the specified <see cref="IFuture{T}"/> again immediately
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="count">The maximum number of retries</param>
        /// <param name="scheduler">A scheduler to retry on</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns a result as soon as one of the retries succeeds and fails with the error of the first try once all retries failed.</returns>
        public static IFuture<T> Retry<T>(this IFuture<T> source, int count, IScheduler scheduler = null)
        {
            return source.Retry(Enumerable.Repeat(TimeSpan.Zero, count), scheduler);
        }

        /// <summary>
        /// Retries the operation by subscribing to the specified <see cref="IFuture{T}"/> again immediately
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> which is watched for errors</param>
        /// <param name="scheduler">A scheduler to retry on</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns a result as soon as the retry succeeds and fails with the error of the first try once the retry fails.</returns>
        public static IFuture<T> Retry<T>(this IFuture<T> source, IScheduler scheduler = null)
        {
            return source.Retry(new[] { TimeSpan.Zero }, scheduler);
        }
    }
}