namespace Futures
{
    using System;

    public static partial class Future
    {
        public static Func<IFuture<TOut>> Then<TIn, TOut>(this Func<IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return () =>
            {
                try
                {
                    return factory().Then(continuation);
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }

        public static Func<TArg1, IFuture<TOut>> Then<TArg1, TIn, TOut>(this Func<TArg1, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return arg1 => ((Func<IFuture<TIn>>)(() => factory(arg1))).Then(continuation)();
        }

        public static Func<TArg1, TArg2, IFuture<TOut>> Then<TArg1, TArg2, TIn, TOut>(
            this Func<TArg1, TArg2, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2) => ((Func<TArg1, IFuture<TIn>>)((a) => factory(a, arg2))).Then(continuation)(arg1);
        }

        public static Func<TArg1, TArg2, TArg3, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3) => ((Func<TArg1, TArg2, IFuture<TIn>>)((a, b) => factory(a, b, arg3))).Then(continuation)(arg1, arg2);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4) => ((Func<TArg1, TArg2, TArg3, IFuture<TIn>>)((a, b, c) => factory(a, b, c, arg4))).Then(continuation)(arg1, arg2, arg3);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5) => ((Func<TArg1, TArg2, TArg3, TArg4, IFuture<TIn>>)((a, b, c, d) => factory(a, b, c, d, arg5))).Then(continuation)(arg1, arg2, arg3, arg4);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, IFuture<TIn>>)((a, b, c, d, e) => factory(a, b, c, d, e, arg6))).Then(continuation)(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IFuture<TIn>>)((a, b, c, d, e, f) => factory(a, b, c, d, e, f, arg7))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IFuture<TIn>>)((a, b, c, d, e, f, g) => factory(a, b, c, d, e, f, g, arg8))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IFuture<TIn>>)((a, b, c, d, e, f, g, h) => factory(a, b, c, d, e, f, g, h, arg9))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i) => factory(a, b, c, d, e, f, g, h, i, arg10))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j) => factory(a, b, c, d, e, f, g, h, i, j, arg11))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j, k) => factory(a, b, c, d, e, f, g, h, i, j, k, arg12))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j, k, l) => factory(a, b, c, d, e, f, g, h, i, j, k, l, arg13))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j, k, l, m) => factory(a, b, c, d, e, f, g, h, i, j, k, l, m, arg14))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j, k, l, m, n) => factory(a, b, c, d, e, f, g, h, i, j, k, l, m, n, arg15))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IFuture<TOut>> Then<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TIn, TOut>(
            this Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IFuture<TIn>> factory,
            Func<TIn, TOut> continuation)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => ((Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IFuture<TIn>>)((a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) => factory(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, arg16))).Then(continuation)(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
    }
}