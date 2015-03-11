namespace Futures
{
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;

    public partial class Future
    {
        /// <summary>
        /// Causes the subscriptions to be executed on the specified scheduler
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The future to modify</param>
        /// <param name="scheduler">The scheduler to execute subscriptions on</param>
        /// <returns>A future which executes <see cref="IFuture{T}#Subscribe"/> on the specified scheduler</returns>
        public static IFuture<T> SubscribeOn<T>(this IFuture<T> source, IScheduler scheduler)
        {
            return Create<T>(
                o =>
                    {
                        var scheduledAction = new MultipleAssignmentDisposable();
                        var result = new CompositeDisposable(scheduledAction);

                        scheduledAction.Disposable = scheduler.Schedule(() => result.Add(source.Subscribe(o)));

                        return result;
                    });
        }

        /// <summary>
        /// Causes the result callbacks of the future to be executed on the specified scheduler
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="source">The future to modify</param>
        /// <param name="scheduler">The scheduler to execute result callbacks on</param>
        /// <returns>A future which executes <see cref="IFuture{T}#OnDone"/> and <see cref="IFuture{T}#OnError"/> on the specified scheduler</returns>
        public static IFuture<T> ObserveOn<T>(this IFuture<T> source, IScheduler scheduler)
        {
            return Create<T>(
                o =>
                    {
                        var result = new CompositeDisposable();
                        result.Add(
                            source.Subscribe(
                                r => result.Add(scheduler.Schedule(() => o.OnDone(r))),
                                ex => result.Add(scheduler.Schedule(() => o.OnError(ex)))));

                        return result;
                    });
        } 
    }
}