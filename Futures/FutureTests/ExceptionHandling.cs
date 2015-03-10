namespace FutureTests
{
    using System;
    using System.Reactive;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Notification = Futures.Notification;

    [TestClass]
    public class ExceptionHandling
    {
        [TestMethod]
        public void RecoveringFromExceptions()
        {
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var sut = future.Recover<Unit, InvalidOperationException>(_ => Unit.Default);

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            sut.Subscribe(recorder);
            future.SetResult(Unit.Default);

            sut = future.Recover<Unit, InvalidOperationException>(_ =>
            {
                throw ex;
                return Unit.Default;
            });

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnDone(), Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RecoveringFromExceptionsWithoutCaringForTheException()
        {
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var sut = future.Recover<Unit, InvalidOperationException>(() => Unit.Default);

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            sut.Subscribe(recorder);
            future.SetResult(Unit.Default);

            sut = future.Recover<Unit, InvalidOperationException>(() =>
            {
                throw ex;
                return Unit.Default;
            });

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnDone(), Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RecoveringFromExceptionsWithFutureFactory()
        {
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var sut = future.Recover<Unit, InvalidOperationException>(_ => Future.Return(Unit.Default));

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            sut.Subscribe(recorder);
            future.SetResult(Unit.Default);

            sut = future.Recover<Unit, InvalidOperationException>(_ => Future.Fail<Unit>(ex));
            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut = future.Recover<Unit, InvalidOperationException>(
                _ =>
                {
                    throw ex;
                    return Future.Never<Unit>();
                });

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RecoveringFromExceptionsWithFutureFactoryIgnoringTheException()
        {
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var sut = future.Recover<Unit, InvalidOperationException>(() => Future.Return(Unit.Default));

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            sut.Subscribe(recorder);
            future.SetResult(Unit.Default);

            sut = future.Recover<Unit, InvalidOperationException>(() => Future.Fail<Unit>(ex));
            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut = future.Recover<Unit, InvalidOperationException>(
                () =>
                {
                    throw ex;
                    return Future.Never<Unit>();
                });

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RecoveringFromExceptionsWithGivenFuture()
        {
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var sut = future.Recover<Unit, InvalidOperationException>(Future.Return(Unit.Default));

            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            sut.Subscribe(recorder);
            var ex = new NotImplementedException();
            future.SetError(ex);

            sut.Subscribe(recorder);
            future.SetResult(Unit.Default);

            sut = future.Recover<Unit, InvalidOperationException>(Future.Fail<Unit>(ex));
            sut.Subscribe(recorder);
            future.SetError(new InvalidOperationException());

            recorder.Events.Should().Equal(Notification.OnDone(), Notification.OnError<Unit>(ex), Notification.OnDone(), Notification.OnError<Unit>(ex));
        }
    }
}