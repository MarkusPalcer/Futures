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

    [TestMethod]
    public void WrapFactoryWithParams15WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams14WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j, k, l, m, n) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams13WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j, k, l, m) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams12WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j, k, l) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams11WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j, k) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams10WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i, j) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams9WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h, i) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8, 9);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams8WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g, h) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7, 8);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams7WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f, g) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6, 7);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams6WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e, f) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5, 6);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams5WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, int, IFuture<int>> factory = (a, b, c, d, e) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4, 5);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams4WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, int, IFuture<int>> factory = (a, b, c, d) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3, 4);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams3WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, int, IFuture<int>> factory = (a, b, c) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2, 3);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams2WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, int, IFuture<int>> factory = (a, b) => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1, 2);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }

    [TestMethod]
    public void WrapFactoryWithParams1WithFailingWrapper()
    {
      // Setup
      var ex = new InvalidOperationException();
      Func<int, IFuture<int>> factory = a => Future.Return(2);
      var newFactory = factory.Wrap(future =>
      {
        throw ex;
        // ReSharper disable once CSharpWarnings::CS0162
        return future;
      });

      // Act
      var newFuture = newFactory(1);
      var observer = new TestObserver<int>();
      newFuture.Subscribe(observer);

      // Assert
      observer.Events.Should().Equal(new[] { Notification<int>.OnError(ex) });
    }
  }
}