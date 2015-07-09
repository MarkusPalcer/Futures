using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace Futures
{
    using System;
    using System.Reactive.Linq;

    public static partial class Future
    {
        /// <summary>
        /// Converts the <see cref="IFuture{T}"/> to an <see cref="IObservable{T}"/> which returns exactly one item and then completes.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="future">The future to convert</param>
        /// <returns>An <see cref="IObservable{T}"/> which returns the result of the future and then terminates.</returns>
        public static IObservable<T> ToObservable<T>(this IFuture<T> future)
        {
            return Observable.Create<T>(observer =>
                {
                    var futureObserver = FutureObserver.Create<T>(
                        result =>
                        {
                            observer.OnNext(result);
                            observer.OnCompleted();
                        },
                        observer.OnError);

                    return future.Subscribe(futureObserver);
                });
        }

        /// <summary>
        /// Unboxes the value from the futures in the given observable
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="source">The observable sequence to unbox the futures from</param>
        /// <returns>An observable sequence which contains the result of the futures in the order that they return their results</returns>
        public static IObservable<T> Unbox<T>(this IObservable<IFuture<T>> source)
        {
            return source.SelectMany(ToObservable);
        }

        /// <summary>
        /// Subscribes to the future repeatedly with a delay generated from the last result.
        /// </summary>
        /// <typeparam name="T">The result type of the future</typeparam>
        /// <param name="future">The future to subscribe to</param>
        /// <param name="scheduler">The scheduler to schedule actions on. If none is specified, the default scheduler is used</param>
        /// <param name="delaySelector">A function to determine the time to wait before subscribing again after a value has been created</param>
        /// <returns>An observable sequence created by repeatedly subscribing to the future.</returns>
        public static IObservable<T> Repeat<T>(this IFuture<T> future, 
            IScheduler scheduler = null,
            Func<T, TimeSpan> delaySelector = null)
        {
            scheduler = scheduler ?? Scheduler.Default;
            delaySelector = delaySelector ?? (_ => TimeSpan.Zero);
            return Observable.Create<T>(o =>
            {
                var currentStepDisposable = new MultipleAssignmentDisposable();
                var b = new BooleanDisposable();
                var result = new CompositeDisposable(b, currentStepDisposable);

                Action nextStep = null;

                Action<T> valueCreated = x =>
                {
                    o.OnNext(x);
                    if (b.IsDisposed) return;

                    // ReSharper disable once AccessToModifiedClosure - Change of closure intended
                    currentStepDisposable.Disposable = scheduler.Schedule(delaySelector(x), nextStep);
                };

                nextStep = () =>
                {
                    currentStepDisposable.Disposable = future.Subscribe(valueCreated, o.OnError);
                };

                currentStepDisposable.Disposable = scheduler.Schedule(nextStep);

                return result;
            });
        }

        /// <summary>
        /// Subscribes to the future repeatedly with a delay generated from the last result.
        /// </summary>
        /// <typeparam name="T">The result type of the future</typeparam>
        /// <param name="future">The future to subscribe to</param>
        /// <param name="scheduler">The scheduler to schedule actions on. If none is specified, the default scheduler is used</param>
        /// <param name="delay">The delay to wait before subscribing to the future again</param>
        /// <returns>An observable sequence created by repeatedly subscribing to the future.</returns>
        public static IObservable<T> Repeat<T>(this IFuture<T> future,
            IScheduler scheduler = null,
            TimeSpan delay = default(TimeSpan))
        {
            return future.Repeat(scheduler, x => delay);
        }

    }
}