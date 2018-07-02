# Memoizer

Provides Memoize C# functions easily.  
No dependencies, targets [.NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md).  

# Why ?

[From Wikipedia:](https://en.wikipedia.org/wiki/Memoization)  
_In computing, memoization [...] is an optimization technique used primarily to speed up computer programs by storing the results of expensive function calls and returning the cached result when the same inputs occur again._

Essentially we want to avoid executing a function again if its inputs do not change.  
This repository provides helper methods to memoize a function that can take up to 2 parameters (for now).

Also most of NuGet packages out there are outdated and do not target .NET Standard so they cannot be used with .NET Core.  

# How do I use it ?

Add an `using` for Memoization namespace.  
Call `Memoizer.Memoize(fn)` and pass it the function that you want to memoize.  
You will receive back and instance of `Memoizer` which you can use to control memoization for your function.  
Invoke the method `Call` from the `Memoizer` instance to execute the memoized function.  

Code example:

```csharp

using Memoization;

...

public string SomeFunctionThatYouWantToMemoize(string someArgs) {
  ...
}

//Memoize the function
var memoizedFn = Memoizer.Memoize(SomeFunctionThatYouWantToMemoize);

//Call the memoized function
var result = memoizedFn.Call("Something");

```

A `Memoizer` instance can also be implicitly converted to a Func so you can use it more transparently.  
What you get is essentially a reference to `Memoizer.Call` that you can still control from the `Memoizer` instance.  

```csharp

//Memoize the function
var memoizedFn = Memoizer.Memoize(SomeFunctionThatYouWantToMemoize);

//Convert the Memoizer instance to a `Func` implictily
Func<string, string> memoizedFnAsFunc = memoizedFn;

```

Another thing to take into consideration is that **memoization might generate memory leaks**.  
In case you want to clear any memoized parameter you can call `Reset` on a `Memoizer` instance.  
This is also useful if you wish to execute the function again.

# Tips on function memoization

_Memoization works best if the parameters are immutable_.  
Having immutable parameters allows us to compare them with a simple reference equal, improving performance.

_A memoized function should not have side effects._  
Memoization will prevent the function from executing if the inputs are the same therefore if you depend on some side effects resulting from the function execution you will lose them.  

_A memoized function should be pure._  
That means that the function result is the same if the inputs are the same, if the function was to depend on other context it would be impossible to memoize correctly as only parameters can be cached. 

# Limitations

Currently the following Func delegates are supported, the others will come in the future:

- [x] Func<T1, TOut>
- [x] Func<T1, T2, TOut>
- [ ] Func<T1, T2, T3, TOut>
- [ ] Func<T1, T2, T3, T4, TOut>
- [ ] Func<T1, T2, T3, T4, T5, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>
- [ ] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>

