namespace FutureTests
{
    using System;
    using System.Reactive;

    using FluentAssertions;

    using Futures;

    using Microsoft.Reactive.Testing;
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

        [TestMethod]
        public void RetryingWithVariableDelay()
        {
            var delays = new[]
                             {
                                 TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(2), TimeSpan.FromMilliseconds(3)
                             };

            var scheduler = new TestScheduler();
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var ex = new NotImplementedException();

            var sut = future.Retry(delays, scheduler);

            sut.Subscribe(recorder);

            future.SetResult(Unit.Default);
            recorder.Events.Should().Equal(Notification.OnDone());


            recorder = new TestObserver<Unit>();
            sut.Subscribe(recorder);
            future.Observers.Should().HaveCount(1);

            future.SetError(ex);
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(2).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RetryingWithFixedDelay()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var ex = new NotImplementedException();

            var sut = future.Retry(TimeSpan.FromMilliseconds(2), 3, scheduler);

            sut.Subscribe(recorder);
            future.Observers.Should().HaveCount(1);

            future.SetError(ex);
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));            
        }

        [TestMethod]
        public void RetryingOnceWithDelay()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var ex = new NotImplementedException();

            var sut = future.Retry(TimeSpan.FromMilliseconds(2), scheduler);

            sut.Subscribe(recorder);
            future.Observers.Should().HaveCount(1);

            future.SetError(ex);
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));   
        }

        [TestMethod]
        public void RetryingMultipleTimesImmediately()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var ex = new NotImplementedException();

            var sut = future.Retry(2, scheduler);
            sut.Subscribe(recorder);
            future.Observers.Should().HaveCount(1);

            future.SetError(ex);
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));
        }

        [TestMethod]
        public void RetryingOnceImmediately()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<Unit>();
            var recorder = new TestObserver<Unit>();
            var ex = new NotImplementedException();

            var sut = future.Retry(scheduler);
            sut.Subscribe(recorder);
            future.Observers.Should().HaveCount(1);

            future.SetError(ex);
            recorder.Events.Should().BeEmpty();
            future.Observers.Should().HaveCount(0);
            scheduler.AdvanceBy(TimeSpan.FromMilliseconds(1).Ticks);
            future.Observers.Should().HaveCount(1);

            future.SetError(new InvalidOperationException());
            recorder.Events.Should().Equal(Notification.OnError<Unit>(ex));
        }
    }
}