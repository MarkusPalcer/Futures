# What to do when the future finishes?

The generic way of handling the futures result is by employing the Observable/Observer-pattern:
An observer can subscribe to the events of the future and will be notified when the future either produced a result or an error occured while processing.
For this the interface `IFuture<T>` contains a method `Subscribe` which takes an `IFutureObserver<T>`.
The observer has two methods which are used by the future to indicate success or failure: `OnDone` and `OnError`.
`OnDone` will be executed with the result of the future as parameter, while `OnError` will be executed with the exception that occurred.

Usually you don't want to implement `IFutureObserver<T>` by yourself, so a static factory method exists which will create an observer from one or two callbacks.
If the second callback is missing, `OnError` will cause nothing to happen.

Example:

```C#
var observer = FutureObserver.Create<string>(Console.WriteLine, ex => Console.WriteLine(ex.Message));
var subscription = myFuture.Subscribe(observer);
```

For your convenience there is an extension method which overloads `Subscribe` to directly accept the two callbacks:

```C#
var subscription = myFuture.Subscribe(ConsoleWriteLine, ex => Console.WriteLine(ex.Message));
```

Basically subscribing to a future works the same as subscribing to an observable sequence with the only difference that `OnNext` and `OnCompleted` are merged into `OnDone`, because a future only represents a single result.

If you use an API that expects you to hand it a task, you can also convert the future into a task by using the extension method `ToTask`.
This conversion can take a `CancellationToken`, which will cause the subscription to be disposed when cancelled.

Usually you don't want to just subscribe to a future, so in the next chapter we'll talk about how we can do some operations on futures.

* [Previous Chapter](ObtainingFutures.md)
* [Next Chapter](Transformations.md)
* [Back to table of contents](README.md) 
* [Back to documentation root](../README.md)