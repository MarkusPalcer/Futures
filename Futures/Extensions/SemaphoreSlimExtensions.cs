using System;
using System.Reactive.Disposables;
using System.Threading;

namespace Futures
{
  internal static class SemaphoreSlimExtensions
  {
    public static IDisposable GetToken(this SemaphoreSlim sem)
    {
      sem.Wait();
      return Disposable.Create(() => sem.Release());
    }
  }
}