namespace Futures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;

    public static partial class Future
    {
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

        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<T> handler) where TEx : Exception
        {
            return source.Recover<T, TEx>(_ => handler());
        }

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

        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, Func<IFuture<T>> handler) where TEx : Exception
        {
            return source.Recover<T, TEx>(_ => handler());
        }

        public static IFuture<T> Recover<T, TEx>(this IFuture<T> source, IFuture<T> recovery) where TEx : Exception
        {
            return source.Recover<T, TEx>(() => recovery);
        }

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

        public static IFuture<T> Retry<T>(this IFuture<T> source, TimeSpan delay, int count, IScheduler scheduler = null)
        {
            return source.Retry(Enumerable.Repeat(delay, count), scheduler);
        }

        public static IFuture<T> Retry<T>(this IFuture<T> source, TimeSpan delay, IScheduler scheduler = null)
        {
            return source.Retry(new[] { delay }, scheduler);
        }

        public static IFuture<T> Retry<T>(this IFuture<T> source, int count, IScheduler scheduler = null)
        {
            return source.Retry(Enumerable.Repeat(TimeSpan.Zero, count), scheduler);
        }

        public static IFuture<T> Retry<T>(this IFuture<T> source, IScheduler scheduler = null)
        {
            return source.Retry(new[] { TimeSpan.Zero }, scheduler);
        }
    }
}