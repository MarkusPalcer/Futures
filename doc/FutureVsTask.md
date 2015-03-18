# Futures vs. Tasks

This chapter is to show the differences between `Task` and `IFuture`.

At a first glance `Task` and `IFuture` seem to be a different syntax for the same thing.
Both encapsulate a piece of work which might already be finished or will be finished some time in the future.
But futures that act like tasks are only one half of the world of futures.

Just like there are two flavors of observables (hot and cold observables) there are two flavors of futures.
To avoid confusion, I will call them "cached" and "uncached" futures.

Cached futures are rather similar to tasks.
They try to produce their value only once and once their value is produced or an error has occured while doing so, this result is reported to subsequent subscribers immediately.
However while 99% of the time you hold a task in your hands, it will already be running, a future might lie dormant until the first subscriber subscribes to it and start its load of work then.
There is no way to ask a future if it is already running or if it is already completed for code that consumes a future should not be dependent on the futures state.
The only thing it can expect from a future is that it starts its work not later than when it subscribes to it.

Uncached futures are more similar to functions that return tasks.
Each time a subscription is made to them they start a new package of work for that subscription and return the result of that package to the subscriber.
In many cases it does not make a difference if the future is cached or not, but some extension methods only make sense with uncached futures.
Thus they are what you should expect to get when you receive a future from a closed source library.
On the other hand when you program a library, you should try to always hand out uncached futures to the caller and if you don't make a note in the remarks of your MSDoc documentation.
Of course you can always turn a uncached future into a not yet running cached future with `Cache` and into an already running cached future with `Prefetch`.