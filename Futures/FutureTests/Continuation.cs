namespace FutureTests
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;

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
                .Equal(Futures.Notification.OnDone(0), Futures.Notification<int>.OnError(ex), Futures.Notification<int>.OnDone(1));
            counter.Should().Be(2);

            // This should change nothing
            sut.Subscribe(recorder).Dispose();
            future.SetResult(1);

            recorder.Events.Should()
                .Equal(Futures.Notification.OnDone(0), Futures.Notification<int>.OnError(ex), Futures.Notification<int>.OnDone(1));
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

            recorder.Events.Should().Equal(Futures.Notification.OnError<int>(ex));
        }

        [TestMethod]
        public void ContinuingWithFunctionsThatCreateFutures()
        {
            var outerFuture = new TestFuture<int>();
            var continuation1 = outerFuture.Then(x => Future.Return(x));

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
                    Futures.Notification.OnDone(1),
                    Futures.Notification.OnError<int>(ex),
                    Futures.Notification.OnError<int>(ex),
                    Futures.Notification.OnError<int>(ex));
        }

        [TestMethod]
        public void ContinuingWithFunctionThatIgnoresResult()
        {
            var future = new TestFuture<int>();
            var counter = 0;
            var sut = future.Then(() => counter++);

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
                .Equal(Futures.Notification.OnDone(0), Futures.Notification.OnError<int>(ex), Futures.Notification.OnDone(1));
            counter.Should().Be(2);

            // This should change nothing
            sut.Subscribe(recorder).Dispose();
            future.SetResult(1);

            recorder.Events.Should()
                .Equal(Futures.Notification.OnDone(0), Futures.Notification.OnError<int>(ex), Futures.Notification.OnDone(1));
            counter.Should().Be(2);
        }

        [TestMethod]
        public void ContinuingWithFunctionThatIgnoresResultAndCreatesFutures()
        {
            var future = new TestFuture<int>();
            var counter = 0;
            var sut = future.Then(() => Future.Return(counter++));

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
                .Equal(Futures.Notification.OnDone(0), Futures.Notification.OnError<int>(ex), Futures.Notification.OnDone(1));
            counter.Should().Be(2);

            // This should change nothing
            sut.Subscribe(recorder).Dispose();
            future.SetResult(1);

            recorder.Events.Should()
                .Equal(Futures.Notification.OnDone(0), Futures.Notification.OnError<int>(ex), Futures.Notification.OnDone(1));
            counter.Should().Be(2);
        }


        [TestMethod]
        public void ContinuingWithAction()
        {
            var future = new TestFuture<int>();
            var items = new List<int>();
            var ex = new NotImplementedException();
            var recorder = new TestObserver<Unit>();

            Action<int> throwingAction = x => { throw ex; };

            var continuation1 = future.Then(items.Add);
            var continuation2 = future.Then(throwingAction);

            // This should produce an OnDone(Unit)
            continuation1.Subscribe(recorder);
            future.SetResult(1);

            // This should produce an OnError(ex)
            continuation2.Subscribe(recorder);
            future.SetResult(2);

            // This should also produce an OnError(ex)
            continuation1.Subscribe(recorder);
            future.SetError(ex);

            recorder.Events.Should()
                .Equal(
                    Futures.Notification.OnDone(),
                    Futures.Notification.OnError<Unit>(ex),
                    Futures.Notification.OnError<Unit>(ex));

            items.Should().Equal(1);
        }

        [TestMethod]
        public void ContinuingWithActionThatIgnoresResult()
        {
            var future = new TestFuture<int>();
            var counter = 0;
            var ex = new NotImplementedException();
            var recorder = new TestObserver<Unit>();

            Action countingAction = () => counter++;
            Action throwingAction = () => { throw ex; };

            var continuation1 = future.Then(countingAction);
            var continuation2 = future.Then(throwingAction);

            // This should produce an OnDone(Unit)
            continuation1.Subscribe(recorder);
            future.SetResult(1);

            // This should produce an OnError(ex)
            continuation2.Subscribe(recorder);
            future.SetResult(2);

            // This should also produce an OnError(ex)
            continuation1.Subscribe(recorder);
            future.SetError(ex);

            recorder.Events.Should()
                .Equal(
                    Futures.Notification.OnDone(),
                    Futures.Notification.OnError<Unit>(ex),
                    Futures.Notification.OnError<Unit>(ex));

            counter.Should().Be(1);
        }
    }
}