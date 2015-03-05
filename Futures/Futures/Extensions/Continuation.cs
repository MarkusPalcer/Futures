using System;

namespace Futures
{
    public static partial class Future
    {
        public static IFuture<TOut> Then<TIn, TOut>(this IFuture<TIn> source, Func<TIn, TOut> continuation)
        {
            return Create<TOut>(observer => source.Subscribe(result =>
            {
                TOut newValue;
                try
                {
                    newValue = continuation(result);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                    return;
                }

                observer.OnDone(newValue);
            }, observer.OnError));
        }
    }
}