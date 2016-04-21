namespace Futures
{
    using System;

    public static partial class Future
    {
        public static Func<IFuture<TOut>> Wrap<TIn, TOut>(
            this Func<IFuture<TIn>>  factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return () =>
            {
                try
                {
                    return wrapper(factory());
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, IFuture<TOut>> Wrap<T1, TIn, TOut>(
            this Func<T1, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return a =>
            {
                try
                {
                    return wrapper(factory(a));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, IFuture<TOut>> Wrap<T1, T2, TIn, TOut>(
            this Func<T1, T2, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b) =>
            {
                try
                {
                    return wrapper(factory(a, b));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, IFuture<TOut>> Wrap<T1, T2, T3, TIn, TOut>(
            this Func<T1, T2, T3, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c) =>
            {
                try
                {
                    return wrapper(factory(a, b, c));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, IFuture<TOut>> Wrap<T1, T2, T3, T4, TIn, TOut>(
            this Func<T1, T2, T3, T4, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j, k) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j, k));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j, k, l) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j, k, l));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j, k, l, m) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j, k, l, m));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j, k, l, m, n) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j, k, l, m, n));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IFuture<TOut>> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TIn, TOut>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) =>
            {
                try
                {
                    return wrapper(factory(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }
    }
}