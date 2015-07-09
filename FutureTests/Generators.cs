namespace FutureTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Disposables;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Generators
    {
        [TestMethod]
        public void Return()
        {
            var sut = Future.Return("123");

            var tester = new TestObserver<string>();

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification.OnDone("123") }, tester.Events.ToArray());

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification.OnDone("123"), Futures.Notification.OnDone("123") }, tester.Events.ToArray());
        }

        [TestMethod]
        public void Fail()
        {
            var ex = new NotImplementedException();
            var sut = Future.Fail<string>(ex);

            var tester = new TestObserver<string>();

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification<string>.OnError(ex) }, tester.Events.ToArray());

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification<string>.OnError(ex), Futures.Notification<string>.OnError(ex) }, tester.Events.ToArray());
        }

        [TestMethod]
        public void Never()
        {
            var sut = Future.Never<string>();

            var tester = new TestObserver<string>();

            sut.Subscribe(tester);

            Assert.AreEqual(0, tester.Events.Count);

            sut.Subscribe(tester);

            Assert.AreEqual(0, tester.Events.Count);
        }

        [TestMethod]
        public void DoneIsSent()
        {
            IFutureObserver<int> observer = new TestObserver<int>();

            var sut = Future.Create<int>(
                o =>
                {
                    observer = o;
                    return Disposable.Empty;
                });

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            observer.OnDone(1);

            recorder.Events.Should().Equal(Futures.Notification.OnDone(1));
        }

        [TestMethod]
        public void ErrorIsSent()
        {
            IFutureObserver<int> observer = new TestObserver<int>();

            var ex = new NotImplementedException();

            var sut = Future.Create<int>(
                o =>
                {
                    observer = o;
                    return Disposable.Empty;
                });

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            observer.OnError(ex);

            recorder.Events.Should().Equal(Futures.Notification<int>.OnError(ex));
        }

        [TestMethod]
        public void AfterUnsubscribingNoDoneIsSent()
        {
            IFutureObserver<int> observer = new TestObserver<int>();

            var sut = Future.Create<int>(
                o =>
                    {
                        observer = o;
                        return Disposable.Empty;
                    });

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);

            subscription.Dispose();

            observer.OnDone(1);

            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void AfterUnsubscribingNoErrorIsSent()
        {
            IFutureObserver<int> observer = new TestObserver<int>();

            var sut = Future.Create<int>(
                o =>
                {
                    observer = o;
                    return Disposable.Empty;
                });

            var recorder = new TestObserver<int>();
            var subscription = sut.Subscribe(recorder);

            subscription.Dispose();

            observer.OnError(new NotImplementedException());

            recorder.Events.Should().BeEmpty();
        }

        [TestMethod]
        public void ExceptionsInCreationAreSendNotThrown()
        {
            var ex = new NotImplementedException();
            var sut = Future.Create<int>(
                o =>
                    {
                        throw ex;
                        return Disposable.Empty;
                    });

            var recorder = new TestObserver<int>();
            sut.Subscribe(recorder);

            recorder.Events.Should().Equal(Futures.Notification<int>.OnError(ex));
        }

        [TestMethod]
        public void EachSubscriberGetsItsOwnEvents()
        {
            var observers = new List<IFutureObserver<int>>();
            var invokes = 0;

            var sut = Future.Create<int>(observer =>
                {
                    observers.Add(observer);
                    invokes++;
                    return Disposable.Empty;
                });

            var observer1 = new TestObserver<int>();
            var observer2 = new TestObserver<int>();
            var observer3 = new TestObserver<int>();

            sut.Subscribe(observer3);
            sut.Subscribe(observer1);
            sut.Subscribe(observer2);

            Assert.AreEqual(3, invokes);

            // Check if the observers have been added in the correct order
            observers
                .Select((observer, index) =>
                    {
                        observer.OnDone(index);
                        return Unit.Default;
                    })
                .ToArray();

            CollectionAssert.AreEqual(new[] { Futures.Notification.OnDone(0) }, observer3.Events.ToArray());
            CollectionAssert.AreEqual(new[] { Futures.Notification.OnDone(1) }, observer1.Events.ToArray());
            CollectionAssert.AreEqual(new[] { Futures.Notification.OnDone(2) }, observer2.Events.ToArray());
        }

        [TestMethod]
        public void GeneratingFromAction()
        {
            var counter = 0;
            Action source = () => counter++;

            var sut = source.ToFuture();

            counter.Should().Be(0);

            var recorder = new TestObserver<Unit>();

            sut.Subscribe(recorder);

            recorder.Events.Should().Equal(Futures.Notification.OnDone());
        }

        [TestMethod]
        public void GeneratingFromFunction()
        {
            var counter = 0;
            Func<int> source = () => counter++;

            var sut = source.ToFuture();

            counter.Should().Be(0);

            var recorder = new TestObserver<int>();

            sut.Subscribe(recorder);
            sut.Subscribe(recorder);

            recorder.Events.Should().Equal(Futures.Notification.OnDone(0), Futures.Notification.OnDone(1));
        }

        [TestMethod]
        public void GeneratingFromLazy()
        {
            var counter = 0;
            var src = new Lazy<int>(() => counter++);

            var sut = src.ToFuture();

            var recorder = new TestObserver<int>();

            sut.Subscribe(recorder);
            sut.Subscribe(recorder);

            recorder.Events.Should().Equal(Futures.Notification.OnDone(0), Futures.Notification.OnDone(0));
            counter.Should().Be(1);
        }
    }
}
