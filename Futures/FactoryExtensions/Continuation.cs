
using System;

namespace Futures
{
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


        public static Func<T1, IFuture<TOut>> Then<T1, TIn, TOut>(this Func<T1, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1))).Then(continuation)();
        }


        public static Func<T1,T2, IFuture<TOut>> Then<T1,T2, TIn, TOut>(this Func<T1,T2, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2))).Then(continuation)();
        }


        public static Func<T1,T2,T3, IFuture<TOut>> Then<T1,T2,T3, TIn, TOut>(this Func<T1,T2,T3, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4, IFuture<TOut>> Then<T1,T2,T3,T4, TIn, TOut>(this Func<T1,T2,T3,T4, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5, IFuture<TOut>> Then<T1,T2,T3,T4,T5, TIn, TOut>(this Func<T1,T2,T3,T4,T5, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15))).Then(continuation)();
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16, IFuture<TOut>> Then<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16, TIn, TOut>(this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16, IFuture<TIn>> factory, Func<TIn, TOut> continuation)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15,factoryArgument16) => ((Func<IFuture<TIn>>)(() => factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15,factoryArgument16))).Then(continuation)();
        }


    }
}