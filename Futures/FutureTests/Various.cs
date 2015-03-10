namespace FutureTests
{
    using System;
    using System.Reactive;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Notification = Futures.Notification;

    [TestClass]
    public class Various
    {
        [TestMethod]
        public void Flattening()
        {
            var outerFuture = new TestFuture<IFuture<int>>();
            var sut = outerFuture.Flatten();
            var ex = new NotImplementedException();

            var recorder = new TestObserver<int>();

            // This should produce an OnDone(1)
            sut.Subscribe(recorder);
            outerFuture.SetResult(Future.Return(1));

            // This should produce an OnError(ex)
            sut.Subscribe(recorder);
            outerFuture.SetResult(Future.Fail<int>(ex));

            // This should also produce an OnError(ex)
            sut.Subscribe(recorder);
            outerFuture.SetError(ex);

            // So we expect OnDone(1) followed by two times OnError(ex)
            recorder.Events.Should()
                .Equal(
                    Notification.OnDone(1),
                    Notification.OnError<int>(ex),
                    Notification.OnError<int>(ex));
        }

        [TestMethod]
        public void Caching()
        {
            var source = new TestFuture<Unit>();
            var sut = source.Cache();
            var recorder = new TestObserver<Unit>();

            sut.Subscribe(recorder);
            sut.Subscribe(recorder);

            source.Observers.Should().HaveCount(1);

            source.SetResult(Unit.Default);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnDone());
        }

        [TestMethod]
        public void CachingErrors()
        {
            var source = new TestFuture<Unit>();
            var sut = source.Cache();
            var ex = new NotImplementedException();
            var recorder = new TestObserver<Unit>();

            sut.Subscribe(recorder);
            sut.Subscribe(recorder);

            source.Observers.Should().HaveCount(1);

            source.SetError(ex);

            recorder.ResetEvent.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();

            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex), Notification.OnError<Unit>(ex));
        }
    }
}