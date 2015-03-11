namespace FutureTests
{
    using System;
    using System.Linq;
    using System.Reactive;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Notification = Futures.Notification;
    using NotificationKind = Futures.NotificationKind;

    [TestClass]
    public class TaskConversionsWithResult
    {
        [TestMethod]
        public void SuccessIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetResult(1);
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification.OnDone(1));
        }

        [TestMethod]
        public void FailureIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();
            var ex = new NotImplementedException();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetException(ex);
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification.OnError<int>(ex));
        }

        [TestMethod]
        public void CancellationIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetCanceled();
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().HaveCount(1);
            recorder.Events.First().Kind.Should().Be(NotificationKind.OnError);
            recorder.Events.First().Error.Should().BeOfType<TaskCanceledException>();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForCancelledTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.SetCanceled();
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForFailedTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.TrySetException(new NotImplementedException());
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForSucceededTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.SetResult(1);
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void ConvertingFactoriesCreatesNewTasksForEachSubscriber()
        {
            var range = Enumerable.Range(0, 3).ToArray();

            var observers = range.Select(_ => new TestObserver<int>()).ToArray();

            var state = 0;
            Func<Task<int>> factory = () => Task.FromResult(state++);
            var sut = factory.ToFuture();

            var expectedEvents = range.Select(Notification.OnDone).ToArray();

            observers.Zip(
                expectedEvents,
                (observer, expected) =>
                    {
                        sut.Subscribe(observer);
                        observer.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
                        observer.Events.Should().Equal(expected);
                        return Unit.Default;
                    }).ToArray();
        }

        [TestMethod]
        public void ConvertingFactoriesWithCancellationCancelsTheTaskWhenUnsubscribing()
        {
            var token = CancellationToken.None;

            Func<CancellationToken, Task<int>> factory = t =>
                {
                    token = t;
                    return new TaskCompletionSource<int>().Task;
                };

            var sut = factory.ToFuture();

            var subscription = sut.Subscribe(new TestObserver<int>());
            token.IsCancellationRequested.Should().BeFalse();

            subscription.Dispose();
            token.IsCancellationRequested.Should().BeTrue();
        }

        [TestMethod]
        public void ConvertingFuturesToTasksYieldsResult()
        {
            var future = new TestFuture<int>();

            var t = future.ToTask();

            t.IsCompleted.Should().BeFalse();
            t.IsFaulted.Should().BeFalse();
            t.IsCanceled.Should().BeFalse();

            future.SetResult(42);

            t.IsCompleted.Should().BeTrue();
            t.IsFaulted.Should().BeFalse();
            t.IsCanceled.Should().BeFalse();
            t.Result.Should().Be(42);
            t.Exception.Should().BeNull();
        }

        [TestMethod]
        public void ConvertingFuturesToTasksYieldsError()
        {
            var future = new TestFuture<int>();

            var t = future.ToTask();

            t.IsCompleted.Should().BeFalse();
            t.IsFaulted.Should().BeFalse();
            t.IsCanceled.Should().BeFalse();

            future.SetError(new NotImplementedException());

            t.IsCompleted.Should().BeTrue();
            t.IsFaulted.Should().BeTrue();
            t.IsCanceled.Should().BeFalse();
            t.Exception.Should().NotBeNull();
            t.Exception.InnerException.Should().BeOfType<NotImplementedException>();
        }

        [TestMethod]
        public void ConvertingFuturesToTasksIsCancellable()
        {
            var future = new TestFuture<int>();

            future.Observers.Should().BeEmpty();

            var cts = new CancellationTokenSource();

            var t = future.ToTask(cts.Token);

            t.IsCompleted.Should().BeFalse();
            t.IsFaulted.Should().BeFalse();
            t.IsCanceled.Should().BeFalse();

            future.Observers.Should().HaveCount(1);

            cts.Cancel();

            t.IsCompleted.Should().BeTrue();
            t.IsFaulted.Should().BeFalse();
            t.IsCanceled.Should().BeTrue();

            future.Observers.Should().BeEmpty();
        }
    }
} 