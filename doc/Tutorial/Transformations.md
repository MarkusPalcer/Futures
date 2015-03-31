# Unleash the power!

After learning how to create and subscribe to futures, here is the real interesting stuff: Transforming futures.
All transformations only work on the result of the future, reported by `OnDone`.
Failures will be handed down the chain without touching them, so the subscriber will get the first failure that happened in the chain.
This is similar to how thrown exceptions bubble up the call-stack when not caught.

## Just links in the chain: `Then`

Usually you don't want to just take the result of a future and report it to the user.
Most of the tim
In its most basic form, it takes a function which transforms the result of the future into a new value.
It executes the function once the transformed future reports completion with the result and returns a future which will report the resulting value.

Example: 
```C#
IFuture<string> newFuture = myFuture.Then(x => x.ToString());
```

Of course the transformation is not limited to synchronous functions.
If the transformation returns a future, it will be unpacked, so you won't get nested futures.

Example:
```C#
IFuture<string> newFuture = myFuture.Then(x => storage.FetchDataAsync(x).ToFuture());
```

For your convenience overloads exist that ignore the result of the previous future (using it only as trigger).
So for example if you wanted to make a web request, wait for it to finish and then make another one, only using the successful-result of the first request, you could do:

```C#
Func<string, IFuture<string>> request = ...;
IFuture<string> myFuture = request("http://mypage.com/firstRequest").Then(() => request("http://mypage.com/secondRequest"));
```

Also if you don't want to produce an output but just signal completion, you don't need to manually return `Unit.Default` in your transformation.
If you pass an `Action` or `Action<T>`, the resulting future will automatically report `Unit.Default` as result.

Examples:
```C#
IFuture<Unit> myFuture = request("http://mypage.com/firstRequest").Then(x => Console.WriteLine(x));
IFuture<Unit> myFuture2 = request("http://mypage.com/secondRequest").Then(() => Console.WriteLine("Done"));
```

## Schedulers

TBD:

* Brief overview of schedulers
* ObserveOn
* SubscribeOn

## Caching

TBD:

* Cache
* Prefetch

## Uncategorized

TBD:

* Materialize
* _Dematerialize_
* Flatten


* [Previous Chapter](Subscribing.md)
* Next Chapter
* [Back to table of contents](README.md) 
* [Back to documentation root](../README.md)