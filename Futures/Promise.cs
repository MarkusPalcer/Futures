using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Threading;

namespace Futures
{
  public class Promise<T> : IFuture<T>, IFutureObserver<T>
  {
    private readonly SemaphoreSlim sem = new SemaphoreSlim(1);
    private readonly List<IFutureObserver<T>> subscriptions = new List<IFutureObserver<T>>();

    private Notification<T>? notification = null;

    public IDisposable Subscribe(IFutureObserver<T> observer)
    {
      using (sem.GetToken())
      {
        if (notification.HasValue)
        {
          switch (notification.Value.Kind)
          {
            case NotificationKind.OnDone:
              observer.OnDone(notification.Value.Value);
              break;
            case NotificationKind.OnError:
              observer.OnError(notification.Value.Error);
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
          return Disposable.Empty;
        }
        else
        {
          subscriptions.Add(observer);
          return Disposable.Create(() => Unsubscribe(observer));
        }
      }
    }

    private void Unsubscribe(IFutureObserver<T> observer)
    {
      using (sem.GetToken())
      {
        subscriptions.Remove(observer);
      }
    }

    public void OnDone(T result)
    {
      using (sem.GetToken())
      {
        foreach (var observer in subscriptions)
        {
          observer.OnDone(result);
        }
        notification = Notification<T>.OnDone(result);
      }
    }

    public void OnError(Exception exception)
    {
      using (sem.GetToken())
      {
        foreach (var observer in subscriptions)
        {
          observer.OnError(exception);
        }
        notification = Notification<T>.OnError(exception);
      }
    }
  }
}