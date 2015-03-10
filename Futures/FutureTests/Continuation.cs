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

        [TestMethod]
        public void ContinuingWithFunctionsThatCreateFutures()
        {
            var outerFuture = new TestFuture<int>();
            var continuation1 = outerFuture.Then(Future.Return);

            var ex = new NotImplementedException();
            var continuation2 = outerFuture.Then(_ => Future.Fail<int>(ex));


            var continuation3 = outerFuture.Then(
                x =>
                    {
                        throw ex;
                        return Future.Return(x);
                    });

            var recorder = new TestObserver<int>();

            outerFuture.Observers.Should().BeEmpty();

            continuation1.Subscribe(recorder);
            var subscription = continuation1.Subscribe(recorder);

            outerFuture.Observers.Should().HaveCount(2);

            subscription.Dispose();
            outerFuture.Observers.Should().HaveCount(1);

            // This should produce an OnDone(1)
            outerFuture.SetResult(1);

            // This should produce an OnError(ex)
            continuation2.Subscribe(recorder);
            outerFuture.SetResult(2);

            // This should also produce an OnError(ex)
            continuation3.Subscribe(recorder);
            outerFuture.SetResult(3);

            // And this should of course produce the OnErorr(ex)
            continuation1.Subscribe(recorder);
            outerFuture.SetError(ex);

            // So we expect OnDone(1) followed by three times OnError(ex)
            recorder.Events.Should()
                .Equal(
                    Notification<int>.OnDone(1),
                    Notification<int>.OnError(ex),
                    Notification<int>.OnError(ex),
                    Notification<int>.OnError(ex));
        }
    }
}