# Futures vs. Observables

This chapter aims to answer the question "Why use a future when I can use an observable?"

Basically the answer is: To make the effects explicit.

The semantic definition of an `IObservable` is an asynchronous sequence (of 0..n elements).
This means with an observable you do _not_ know how many elements you will get.
Of course if you use `Task.ToObservable`, you always know that you will get exactly one result (or an exception), but the interface does not make this explicit.

`IFuture` aims to make this behavior explicit. 
When you get a future, you always know that you will get exactly one result (or an exception).

Another argument is a bit more aesthetic (and thus in my opinion not as strong and thus a bit longer explained):
.Select(...) on something you know to have only one element does not read very well.
The methods give someone who reads the code the impression that one is operation on a sequence of elements while in truth one is just operating on one and the same element.

With futures you are not writing code which looks like you're querying sequences, but you write code which looks like you are chaining actions together which are usually executed on demand.
This way you can also wire workflows together if you define them as futures.