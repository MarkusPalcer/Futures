
using System;

namespace Futures
{
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
            return (factoryArgument1) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2, IFuture<TOut>> Wrap<T1,T2, TIn, TOut>(
            this Func<T1,T2, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3, IFuture<TOut>> Wrap<T1,T2,T3, TIn, TOut>(
            this Func<T1,T2,T3, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4, IFuture<TOut>> Wrap<T1,T2,T3,T4, TIn, TOut>(
            this Func<T1,T2,T3,T4, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


        public static Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, IFuture<TOut>> Wrap<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, TIn, TOut>(
            this Func<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15, IFuture<TIn>> factory,
            Func<IFuture<TIn>, IFuture<TOut>> wrapper)
        {
            return (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15) =>
            {
                try
                {
                    return wrapper(factory(factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15));
                }
                catch (Exception ex)
                {
                    return Fail<TOut>(ex);
                }
            };
        }


    }
}