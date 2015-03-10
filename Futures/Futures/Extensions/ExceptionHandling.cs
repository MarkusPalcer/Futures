namespace Futures
{
    using System;
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
    }
}