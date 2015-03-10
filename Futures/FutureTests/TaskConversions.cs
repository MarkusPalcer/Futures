namespace FutureTests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskConversions
    {
        [TestMethod]
        public void SuccessIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetResult(1);
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification<int>.OnDone(1));
        }

        [TestMethod]
        public void FailureIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();
            var ex = new NotImplementedException();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetException(ex);
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().Equal(Notification<int>.OnError(ex));
        }

        [TestMethod]
        public void CancellationIsPropagated()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            tcs.SetCanceled();
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().HaveCount(1);
            recorder.Events.First().Kind.Should().Be(NotificationKind.OnError);
            recorder.Events.First().Error.Should().BeOfType<TaskCanceledException>();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForCancelledTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.SetCanceled();
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForFailedTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.TrySetException(new NotImplementedException());
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void NothingIsPropagatedWhenSubscriptionIsCancelledForSucceededTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var sut = tcs.Task.ToFuture();

            var evt = new ManualResetEventSlim();

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);
            sut.Subscribe(_ => evt.Set(), _ => evt.Set());

            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeFalse();
            recorder.Events.Should().BeEmpty();

            subscription.Dispose();

            tcs.SetResult(1);
            evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
            recorder.Events.Should().BeEmpty();
        }
    }
} 