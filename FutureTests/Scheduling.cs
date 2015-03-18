namespace FutureTests
{
    using System;

    using FluentAssertions;

    using Futures;

    using Microsoft.Reactive.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Scheduling
    {
        [TestMethod]
        public void ScheduleOn()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<int>();
            var observer = new TestObserver<int>();

            future.SubscribeOn(scheduler).Subscribe(observer);

            future.Observers.Should().BeEmpty();

            scheduler.AdvanceBy(1);

            future.Observers.Should().HaveCount(1);
        }

        [TestMethod]
        public void ObserveOnWithResult()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<int>();
            var observer = new TestObserver<int>();

            future.ObserveOn(scheduler).Subscribe(observer);

            future.Observers.Should().HaveCount(1);

            future.SetResult(42);

            observer.Events.Should().BeEmpty();

            scheduler.AdvanceBy(1);

            observer.Events.Should().Equal(Notification.OnDone(42));

            future.Observers.Should().BeEmpty();
        }

        [TestMethod]
        public void ObserveOnWithError()
        {
            var scheduler = new TestScheduler();
            var future = new TestFuture<int>();
            var observer = new TestObserver<int>();
            var ex = new NotImplementedException();

            future.ObserveOn(scheduler).Subscribe(observer);

            future.Observers.Should().HaveCount(1);

            future.SetError(ex);

            observer.Events.Should().BeEmpty();

            scheduler.AdvanceBy(1);

            observer.Events.Should().Equal(Notification.OnError<int>(ex));

            future.Observers.Should().BeEmpty();
        }
    }
}