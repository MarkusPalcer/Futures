namespace FutureTests
{
    using System;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Subscription
    {
        [TestMethod]
        public void SubscribingExecutesOnNext()
        {
            object received = null;
            Exception ex = null;

            var sut = Future.Return(10);
            sut.Subscribe(x => received = x, x => ex = x);

            received.Should().Be(10);
            ex.Should().BeNull();
        }

        [TestMethod]
        public void SubscribingExecutesOnError()
        {
            object received = null;
            Exception ex = null;

            var sut = Future.Fail<int>(new NotImplementedException());
            sut.Subscribe(x => received = x, x => ex = x);

            received.Should().BeNull();
            ex.Should().BeOfType<NotImplementedException>();
        }    
    }
}