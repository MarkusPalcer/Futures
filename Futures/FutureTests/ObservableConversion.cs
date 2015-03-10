namespace FutureTests
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Security.Cryptography.X509Certificates;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObservableConversion
    {
        [TestMethod]
        public void EachSubscriptionToTheObservableCreatesOneSubscriptionOnTheFuture()
        {
            var future = new TestFuture<int>();

            var sut = future.ToObservable();

            future.Observers.Should().HaveCount(0);

            sut.Subscribe(Observer.Create<int>(_ => { }));
            future.Observers.Should().HaveCount(1);

            sut.Subscribe(Observer.Create<int>(_ => { }));
            future.Observers.Should().HaveCount(2);

            sut.Subscribe(Observer.Create<int>(_ => { }));
            future.Observers.Should().HaveCount(3);
        }

        [TestMethod]
        public void UnsubscribingFromTheObservableUnsubscribesFromTheFuture()
        {
            var future = new TestFuture<int>();

            var sut = future.ToObservable();
            var subscription = sut.Subscribe(Observer.Create<int>(_ => { }));

            subscription.Dispose();

            future.Observers.Should().BeEmpty();
        }

        [TestMethod]
        public void OnDoneShouldCallOnNextAndOnCompleted()
        {
            var future = new TestFuture<int>();
            var notifications = new List<System.Reactive.Notification<int>>();

            future.ToObservable().Materialize().Subscribe(notifications.Add);

            notifications.Should().BeEmpty();

            future.SetResult(1);

            notifications.Should().Equal(Notification.CreateOnNext(1), Notification.CreateOnCompleted<int>());
        }

        [TestMethod]
        public void OnErrorShouldCallOnError()
        {
            var future = new TestFuture<int>();
            var notifications = new List<System.Reactive.Notification<int>>();

            future.ToObservable().Materialize().Subscribe(notifications.Add);

            notifications.Should().BeEmpty();

            var exception = new NotImplementedException();  
            future.SetError(exception);

            notifications.Should().Equal(Notification.CreateOnError<int>(exception));
        }
    }
}