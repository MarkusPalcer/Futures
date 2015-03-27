# Obtaining Futures

The first section of our tutorial is about how to obtain a future when you don't have a library which already returns one.

## Converting tasks to futures

The most obvious though would be to take a task and convert it into a future, since tasks already do something which is very similar to what futures do.
In order to convert a task to a future, you would simply call the ``ToFuture``-extension on the task.
Alternatively you can pass the task into the static function ``Future.FromTask``.

However using this method is not encouraged due to two reasons:

1. The task will most likely already be running instead of being started on demand
2. You can't re-run the task by resubscribing to the future (which is interesting for automatic retries)
3. The task won't cancel when you unsubscribe

Luckily you can fix this by using two other conversions.
The first one will convert a function which returns a task.
Each time the future is subscribed to, the function is called, returns a running task which then is used to determine what to notify this particular observer.
This will fix the first two issues on our list, since the asynchronous task won't be started until someone subscribes to the future and you can start the asynchronous task by simply subscribing again.
That way you don't need to wire up the continuations (more on those in the next chapter) again.

To automatically cancel the task that is created once the observer unsubscribes from the future you need cancellation support on your original task.
A parameter of the type `CancellationToken` indicates that a function which returns a tasks supports cancellation of the task.
To wire up cancellation to your generated future, wrap the function into the signature `Func<CancellationToken, Task<T>>` and call the `ToFuture` on that.
Each time an observer subscribes to your future, a new cancellation token is created and passed into your delegate.

These conversions work with `Task<T>` as well as `Task`.
The latter is interpreted as `Task<System.Reactive.Unit>` which returns `System.Reactive.Unit.Default` once it completes.

Example:

```C#
SemaphoreSlim sem = new SemaphoreSlim(1);
Func<CancellationToken, Task> factory = sem.WaitAsync;
IFuture<System.Reactive.Unit> future = factory.ToFuture();
```

## Constant futures

Sometimes you might need create a future which has a predefined result.
For these cases three methods exist that let you easily create a future from one of its three possible result behaviors:

* If you want the future to immediately notify being done with a specific value, you can use `Future.Return`.
* If you want the future to immediately fail with an exception, you can use `Future.Fail`
* If you want the future to never complete, you can use `Future.Never`

Note that when you use `Future.Never`, you might cause the program to wait for its result forever.
You're mostly going to use it in tests to test timeout-behavior.

## Converting synchronous code

Futures don't guarantee that the operation they represent runs asynchronously.
The first two constant futures are a good example of this behavior:
Each time the future is subscribed to a notification is made synchronously.
Of course you are able to generate the result for each subscription synchronously.
To do this, simply use the `ToFuture` extension defined on `Func<T>`.
Even better: If your future should only notify that something was done (like a `Task`), you can even use `ToFuture` on `Action`, which will act as if you return `Unit.Default`.

Example:

```C#
Random r = new Random();
IFuture<int> future1 = (() => r.NextInt()).ToFuture();
IFuture<Unit> future2 = (() => r = new Random()).ToFuture();
```

## For experts: Creating your own futures

You don't have to create a class that implements `IFuture<T>` yourself in most cases.
Most of the futures are defined by what happens when they are subscribed to.
You can create such a future by using `Future.Create` and specifying the implementation of the subscribe method.
This is similar to `Observable.Create` in the aspect that it returns a disposable which represents the subscription.
Disposing this disposable is equivalent to unsubscribing from the future.
ReactiveExtensions provides you with implementations of disposable tokens that enable you to handle unsubscription.

Example:

```C#
IFuture<Unit> future = Future.Create(o => {
    EventHandler<EventArgs> successHandler = (sender, args) => o.OnDone(Unit.Default);
    EventHandler<EventArgs> errorHandler = (sender, args) => o.OnError(eventSource.Error);
    eventSource.Event += successHandler;
    eventSource.ErrorOccured += errorHandler;

    return Disposable.Create(() => {
        eventSource.Event -= successHandler;
        eventSource.ErrorOccured -= errorHandler;    
    });
});
```

* Next chapter
* [Back to table of contents](README.md) 
* [Back to documentation root](../README.md)
* [Back to project root](../../)