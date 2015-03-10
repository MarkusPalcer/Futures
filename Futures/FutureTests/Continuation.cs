namespace FutureTests
{
    using System;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Continuation
    {
        [TestMethod]
        public void ContinuingWihtSimpleFunctions()
        {
            var future = new TestFuture<int>();
            var counter = 0;
            var sut = future.Then(i => counter++);

            var recorder = new TestObserver<int>();

            // This should create an OnDone(0)
            sut.Subscribe(recorder);
            future.SetResult(1);

            // This should create an OnError<NotImplementedException>()
            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            // This should create an OnDone(1)
            sut.Subscribe(recorder);
            future.SetResult(1);

            recorder.Events.Should()
                .Equal(Notification<int>.OnDone(0), Notification<int>.OnError(ex), Notification<int>.OnDone(1));
            counter.Should().Be(2);

            // This should change nothing
            sut.Subscribe(recorder).Dispose();
            future.SetResult(1);

            recorder.Events.Should()
                .Equal(Notification<int>.OnDone(0), Notification<int>.OnError(ex), Notification<int>.OnDone(1));
            counter.Should().Be(2);
        }

        [TestMethod]
        public void ContinuingWithThrowingFunctions()
        {
            var future = new TestFuture<int>();
            var ex = new NotImplementedException();
            var sut = future.Then(_ =>
            {
                throw ex;
                return 1;
            });

            var recorder = new TestObserver<int>();

            sut.Subscribe(recorder);
            future.SetResult(1);

            recorder.Events.Should().Equal(Notification<int>.OnError(ex));
        }
    }
}