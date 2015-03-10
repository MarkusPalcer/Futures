namespace FutureTests
{
    using System;

    using FluentAssertions;

    using Futures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Various
    {
        [TestMethod]
        public void Flattening()
        {
            var outerFuture = new TestFuture<IFuture<int>>();
            var sut = outerFuture.Flatten();
            var ex = new NotImplementedException();

            var recorder = new TestObserver<int>();

            // This should produce an OnDone(1)
            sut.Subscribe(recorder);
            outerFuture.SetResult(Future.Return(1));

            // This should produce an OnError(ex)
            sut.Subscribe(recorder);
            outerFuture.SetResult(Future.Fail<int>(ex));

            // This should also produce an OnError(ex)
            sut.Subscribe(recorder);
            outerFuture.SetError(ex);

            // So we expect OnDone(1) followed by two times OnError(ex)
            recorder.Events.Should()
                .Equal(
                    Notification<int>.OnDone(1),
                    Notification<int>.OnError(ex),
                    Notification<int>.OnError(ex));
        }
    }
}