namespace FutureTests
{
    using System;
    using System.Linq;
    using System.Reactive;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NotificationKind = Futures.NotificationKind;

    [TestClass]
    public class TaskConversions
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
            recorder.Events.Should().Equal(Futures.Notification<int>.OnDone(1));
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
            recorder.Events.Should().Equal(Futures.Notification<int>.OnError(ex));
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

            var expectedEvents = range.Select(Futures.Notification<int>.OnDone).ToArray();

            observers.Zip(
                expectedEvents,
                (observer, expected) =>
                    {
                        sut.Subscribe(observer);
                        observer.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
                        observer.Events.Should().Equal(expected);
                        return Unit.Default;
                    }).ToArray();

            foreach (var observer in observers)
            {
                sut.Subscribe(observer);
                observer.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            }
        }
    }
} 