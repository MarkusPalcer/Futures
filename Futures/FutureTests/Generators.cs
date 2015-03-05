using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using Futures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FutureTests
{
    [TestClass]
    public class Generators
    {
        [TestMethod]
        public void Return()
        {
            var sut = Future.Return("123");

            var tester = new TestObserver<string>();

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification<string>.OnDone("123") }, tester.Events.ToArray());

            sut.Subscribe(tester);

            CollectionAssert.AreEqual(new[] { Futures.Notification<string>.OnDone("123"), Futures.Notification<string>.OnDone("123") }, tester.Events.ToArray());
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
        public void CreateFuture()
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
                
            CollectionAssert.AreEqual(new[]{Futures.Notification<int>.OnDone(0)}, observer3.Events.ToArray());
            CollectionAssert.AreEqual(new[]{Futures.Notification<int>.OnDone(1)}, observer1.Events.ToArray());
            CollectionAssert.AreEqual(new[]{Futures.Notification<int>.OnDone(2)}, observer2.Events.ToArray());
        }

        public class TestObserver<T> : IFutureObserver<T>
        {
            private readonly List<Futures.Notification<T>> _events = new List<Futures.Notification<T>>();

            public IReadOnlyList<Futures.Notification<T>> Events { get { return _events; } }

            public void OnDone(T result)
            {
                _events.Add(Futures.Notification<T>.OnDone(result));
            }

            public void OnError(Exception exception)
            {
                _events.Add(Futures.Notification<T>.OnError(exception));
            }
        }
    }
}
