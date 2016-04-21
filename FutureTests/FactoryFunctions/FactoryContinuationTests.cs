using System;
using System.Globalization;
using FluentAssertions;
using Futures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FutureTests.FactoryFunctions
{
  [TestClass]
  public class FactoryContinuationTests
  {
    [TestMethod]
    public void ContinuingNormally()
    {
      // Setup
      Func<IFuture<int>> factory = () => Future.Return(2);
      var newFactory = factory.Then(x => x.ToString(CultureInfo.InvariantCulture));

      // Act
      var newFuture = newFactory();
      var observer = new TestObserver<string>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<string>.OnDone("2") });
    }

    [TestMethod]
    public void ContinuingWithThrowingFunctions()
    {
      // Setup
      Func<IFuture<int>> factory = () => Future.Return(2);
      var ex = new NotImplementedException();
      var newFactory = factory.Then(_ =>
      {
        throw ex;
        return string.Empty;
      });

      // Act
      var newFuture = newFactory();
      var observer = new TestObserver<string>();
      newFuture.Subscribe(observer);

      observer.Events.Should().Equal(Notification.OnError<string>(ex));
    }
  }
}