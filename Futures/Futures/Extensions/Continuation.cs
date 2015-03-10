namespace Futures
{
    using System;
    using System.Reactive.Disposables;

    public static partial class Future
    {
        public static IFuture<TOut> Then<TIn, TOut>(this IFuture<TIn> source, Func<TIn, TOut> continuation)
        {
            return Create<TOut>(
                observer => source.Subscribe(
                    result =>
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
                    },
            observer.OnError));
        }

        public static IFuture<TOut> Then<TIn, TOut>(this IFuture<TIn> source, Func<TIn, IFuture<TOut>> continuation)
        {
            return Create<TOut>(
                observer =>
                {
                    var disp = new MultipleAssignmentDisposable();
                    disp.Disposable = source.Subscribe(
                        result =>
                        {
                            IFuture<TOut> innerFuture;
                            try
                            {
                                innerFuture = continuation(result);
                            }
                            catch (Exception ex)
                            {
                                observer.OnError(ex);
                                return;
                            }

                            disp.Disposable = innerFuture.Subscribe(observer);
                        },
                        observer.OnError);
                    return disp;
                });
        }
    }
}