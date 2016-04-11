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
    public class TaskConversionsWithoutResult
    {
        [TestMethod]
        public void SuccessIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = ((Task)tcs.Task).ToFuture();

            var recorder = new TestObserver<Unit>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetResult(1);
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification.OnDone());
        }

        [TestMethod]
        public void FailureIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = ((Task)tcs.Task).ToFuture();
            var ex = new NotImplementedException();

            var recorder = new TestObserver<Unit>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetException(ex);
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void CancellationIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = ((Task)tcs.Task).ToFuture();

            var recorder = new TestObserver<Unit>();
            sut.Subscribe(recorder);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(1000)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetCanceled();
            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(1000)).Should().BeTrue();
            recorder.Events.Should().HaveCount(1);
            recorder.Events.First().Kind.Should().Be(NotificationKind.OnError);
            recorder.Events.First().Error.Should().BeOfType<TaskCanceledException>();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForCancelledTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = ((Task)tcs.Task).ToFuture();

            var recorder = new TestObserver<Unit>();
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
            var sut = ((Task)tcs.Task).ToFuture();

            var recorder = new TestObserver<Unit>();
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
            var sut = ((Task)tcs.Task).ToFuture();

            var recorder = new TestObserver<Unit>();
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

            var observers = range.Select(_ => new TestObserver<Unit>()).ToArray();

            var state = 0;
            Func<Task> factory = () => Task.FromResult(state++);
            var sut = factory.ToFuture();

            var expectedEvents = range.Select(_ => Notification.OnDone()).ToArray();

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

            Func<CancellationToken, Task> factory = t =>
            {
                token = t;
                return new TaskCompletionSource<int>().Task;
            };

            var sut = factory.ToFuture();

            var subscription = sut.Subscribe(new TestObserver<Unit>());
            token.IsCancellationRequested.Should().BeFalse();

            subscription.Dispose();
            token.IsCancellationRequested.Should().BeTrue();
        }

    }
}