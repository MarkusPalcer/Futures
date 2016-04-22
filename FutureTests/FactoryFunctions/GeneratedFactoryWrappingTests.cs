
using System;
using System.Globalization;
using FluentAssertions;
using Futures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FutureTests.FactoryFunctions
{
    [TestClass]
    public class GeneratedFactoryWrappingTests
    {
        [TestMethod]
        public void WrappingFactoryWith1ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1;

            Func<int, IFuture<int>> factory = (factoryArgument1) =>
            {
            actualArgument1Value = factoryArgument1;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1);
        }

        [TestMethod]
        public void WrappingFactoryWith1Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1;

          // Setup
          Func<int, IFuture<int>> factory = (factoryArgument1) =>
          {
            actualArgument1Value = factoryArgument1;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1);
        }

        [TestMethod]
        public void WrappingFactoryWith2ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1;

            Func<int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2);
        }

        [TestMethod]
        public void WrappingFactoryWith2Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1;

          // Setup
          Func<int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2);
        }

        [TestMethod]
        public void WrappingFactoryWith3ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1;

            Func<int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3);
        }

        [TestMethod]
        public void WrappingFactoryWith3Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1;

          // Setup
          Func<int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3);
        }

        [TestMethod]
        public void WrappingFactoryWith4ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1;

            Func<int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4);
        }

        [TestMethod]
        public void WrappingFactoryWith4Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1;

          // Setup
          Func<int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4);
        }

        [TestMethod]
        public void WrappingFactoryWith5ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1;

            Func<int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5);
        }

        [TestMethod]
        public void WrappingFactoryWith5Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1;

          // Setup
          Func<int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5);
        }

        [TestMethod]
        public void WrappingFactoryWith6ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1;

            Func<int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6);
        }

        [TestMethod]
        public void WrappingFactoryWith6Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1;

          // Setup
          Func<int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6);
        }

        [TestMethod]
        public void WrappingFactoryWith7ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1;

            Func<int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7);
        }

        [TestMethod]
        public void WrappingFactoryWith7Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7);
        }

        [TestMethod]
        public void WrappingFactoryWith8ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1;

            Func<int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8);
        }

        [TestMethod]
        public void WrappingFactoryWith8Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8);
        }

        [TestMethod]
        public void WrappingFactoryWith9ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1;

            Func<int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9);
        }

        [TestMethod]
        public void WrappingFactoryWith9Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9);
        }

        [TestMethod]
        public void WrappingFactoryWith10ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10);
        }

        [TestMethod]
        public void WrappingFactoryWith10Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10);
        }

        [TestMethod]
        public void WrappingFactoryWith11ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11);
        }

        [TestMethod]
        public void WrappingFactoryWith11Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11);
        }

        [TestMethod]
        public void WrappingFactoryWith12ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12);
        }

        [TestMethod]
        public void WrappingFactoryWith12Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12);
        }

        [TestMethod]
        public void WrappingFactoryWith13ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13);
        }

        [TestMethod]
        public void WrappingFactoryWith13Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13);
        }

        [TestMethod]
        public void WrappingFactoryWith14ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1; var actualArgument14Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13; actualArgument14Value = factoryArgument14;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13,14);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13); actualArgument14Value.Should().Be(14);
        }

        [TestMethod]
        public void WrappingFactoryWith14Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1; var actualArgument14Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13; actualArgument14Value = factoryArgument14;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13,14);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13); actualArgument14Value.Should().Be(14);
        }

        [TestMethod]
        public void WrappingFactoryWith15ParametersWithThrowingWrapper()
        {

            // Setup
            var result = new Random().Next();
            var ex = new NotImplementedException();

            var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1; var actualArgument14Value = -1; var actualArgument15Value = -1;

            Func<int,int,int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15) =>
            {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13; actualArgument14Value = factoryArgument14; actualArgument15Value = factoryArgument15;

            return Future.Return(result);
            };

            var newFactory = factory.Wrap(future => {
                throw ex;
                return future;
            });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(Notification.OnError<int>(ex));
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13); actualArgument14Value.Should().Be(14); actualArgument15Value.Should().Be(15);
        }

        [TestMethod]
        public void WrappingFactoryWith15Parameters()
        {
          var result = new Random().Next();
          var actualArgument1Value = -1; var actualArgument2Value = -1; var actualArgument3Value = -1; var actualArgument4Value = -1; var actualArgument5Value = -1; var actualArgument6Value = -1; var actualArgument7Value = -1; var actualArgument8Value = -1; var actualArgument9Value = -1; var actualArgument10Value = -1; var actualArgument11Value = -1; var actualArgument12Value = -1; var actualArgument13Value = -1; var actualArgument14Value = -1; var actualArgument15Value = -1;

          // Setup
          Func<int,int,int,int,int,int,int,int,int,int,int,int,int,int,int, IFuture<int>> factory = (factoryArgument1,factoryArgument2,factoryArgument3,factoryArgument4,factoryArgument5,factoryArgument6,factoryArgument7,factoryArgument8,factoryArgument9,factoryArgument10,factoryArgument11,factoryArgument12,factoryArgument13,factoryArgument14,factoryArgument15) =>
          {
            actualArgument1Value = factoryArgument1; actualArgument2Value = factoryArgument2; actualArgument3Value = factoryArgument3; actualArgument4Value = factoryArgument4; actualArgument5Value = factoryArgument5; actualArgument6Value = factoryArgument6; actualArgument7Value = factoryArgument7; actualArgument8Value = factoryArgument8; actualArgument9Value = factoryArgument9; actualArgument10Value = factoryArgument10; actualArgument11Value = factoryArgument11; actualArgument12Value = factoryArgument12; actualArgument13Value = factoryArgument13; actualArgument14Value = factoryArgument14; actualArgument15Value = factoryArgument15;

            return Future.Return(result);
          };

          bool wrapperCalled = false;
          var newFactory = factory.Wrap(f => {
            wrapperCalled = true;
            return f;
          });

          // Act
          var newFuture = newFactory(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
          var observer = new TestObserver<int>();
          newFuture.Subscribe(observer);

          // Assert
          observer.Events.Should().Equal(new[] { Notification<int>.OnDone(result) });
          actualArgument1Value.Should().Be(1); actualArgument2Value.Should().Be(2); actualArgument3Value.Should().Be(3); actualArgument4Value.Should().Be(4); actualArgument5Value.Should().Be(5); actualArgument6Value.Should().Be(6); actualArgument7Value.Should().Be(7); actualArgument8Value.Should().Be(8); actualArgument9Value.Should().Be(9); actualArgument10Value.Should().Be(10); actualArgument11Value.Should().Be(11); actualArgument12Value.Should().Be(12); actualArgument13Value.Should().Be(13); actualArgument14Value.Should().Be(14); actualArgument15Value.Should().Be(15);
        }

    }
}