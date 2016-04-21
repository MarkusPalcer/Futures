using System;
using System.Globalization;
using FluentAssertions;
using Futures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FutureTests.FactoryFunctions
{
  [TestClass]
  public class WrappingFactoryTests
  {
    [TestMethod]
    public void WrapBasicFactory()
    {
      // Setup
      Func<IFuture<int>> factory = () => Future.Return(2);
      var newFactory = factory.Wrap(future => future.Then(x => x.ToString(CultureInfo.InvariantCulture)));

      // Act
      var newFuture = newFactory();
      var observer = new TestObserver<string>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<string>.OnDone("2") });
    }

    [TestMethod]
    public void WrapFailingFactory()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<IFuture<int>> factory = () => Future.Fail<int>(ex);
      var newFactory = factory.Wrap(future => future.Then(x => x.ToString(CultureInfo.InvariantCulture)));

      // Act
      var newFuture = newFactory();
      var observer = new TestObserver<string>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<string>.OnError(ex) });
    }

    [TestMethod]
    public void WrapWithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<IFuture<int>> factory = () => Future.Return(2);
      var newFactory = factory.Wrap<int,int>(future =>
      {
        throw ex;
      });

      // Act
      var newFuture = newFactory(); // Note: No exception here
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) }); // The exception will be the futures result
    }
  }
}