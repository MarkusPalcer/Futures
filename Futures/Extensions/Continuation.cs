namespace Futures
{
    using System;
    using System.Reactive;
    using System.Reactive.Disposables;
    using System.Runtime.CompilerServices;
    using System.Security.Permissions;

    public static partial class Future
    {
        /// <summary>
        /// Continues the operation by transforming the result of the <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="TIn">The result type of the transformed <see cref="IFuture{T}"/></typeparam>
        /// <typeparam name="TOut">The result type of the transformation</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The transformation to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns the transformed result</returns>
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

        /// <summary>
        /// Continues the operation by transforming the result of the <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="TIn">The result type of the transformed <see cref="IFuture{T}"/></typeparam>
        /// <typeparam name="TOut">The result type of the transformation</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The transformation to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns the end result of the transformation</returns>
        /// <remarks>This overload returns unpacks the <see cref="IFuture{T}"/> that the transformation creates</remarks>
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

        /// <summary>
        /// Continues the operation by executing a function which ignores the result of the original <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="TIn">The result type of the <see cref="IFuture{T}"/> which is to be ignored</typeparam>
        /// <typeparam name="TOut">The result type of the function</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The function to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns the result of the function</returns>
        public static IFuture<TOut> Then<TIn, TOut>(this IFuture<TIn> source, Func<TOut> continuation)
        {
            return source.Then(_ => continuation());
        }

        /// <summary>
        /// Continues the operation by executing a function which ignores the result of the original <see cref="IFuture{T}"/>
        /// </summary>
        /// <typeparam name="TIn">The result type of the <see cref="IFuture{T}"/> which is to be ignored</typeparam>
        /// <typeparam name="TOut">The result type of the function</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The function to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns the result of the function</returns>
        /// <remarks>This overload returns unpacks the <see cref="IFuture{T}"/> that the function creates</remarks>
        public static IFuture<TOut> Then<TIn, TOut>(this IFuture<TIn> source, Func<IFuture<TOut>> continuation)
        {
            return source.Then(_ => continuation());
        }

        /// <summary>
        /// Continues the operation by executing an action
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/></typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The action to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns <see cref="Unit#Default"/> when the operation is completed</returns>
        public static IFuture<Unit> Then<T>(this IFuture<T> source, Action<T> continuation)
        {
            return source.Then(
                x =>
                {
                    continuation(x);
                    return Unit.Default;
                });
        }

        /// <summary>
        /// Continues the operation by executing an action
        /// </summary>
        /// <typeparam name="T">The result type of the <see cref="IFuture{T}"/> which is to be ignored</typeparam>
        /// <param name="source">The <see cref="IFuture{T}"/> to operate on</param>
        /// <param name="continuation">The action to execute</param>
        /// <returns>A <see cref="IFuture{T}"/> which returns <see cref="Unit#Default"/> when the operation is completed</returns>
        public static IFuture<Unit> Then<T>(this IFuture<T> source, Action continuation)
        {
            return source.Then(_ => continuation());
        }
    }
}