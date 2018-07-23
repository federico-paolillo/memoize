[![MIT License](https://img.shields.io/github/license/federico-paolillo/memoize.svg?style=flat-square)](https://github.com/federico-paolillo/memoize/blob/master/LICENSE)
[![Travis branch](https://img.shields.io/travis/federico-paolillo/memoize/master.svg?style=flat-square)](https://travis-ci.org/federico-paolillo/memoize)

# Memoizer

Memoize C# functions with ease.  
No dependencies, targets [.NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md).  
All 16 Func delegates are supported and tested using source code generation.

# Why ?

[From Wikipedia:](https://en.wikipedia.org/wiki/Memoization)  
_In computing, memoization [...] is an optimization technique used primarily to speed up computer programs by storing the results of expensive function calls and returning the cached result when the same inputs occur again._

Essentially we want to avoid executing a function again if its inputs do not change.  
This repository provides helper methods to memoize a function that can take up to 16 parameters.

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
This is useful if you wish to force executing the function again or clear any retained memory.

# Tips on function memoization

_Memoization works best if the parameters are immutable_.  
Having immutable parameters allows us to compare them with a simple reference equal, improving performance.

_A memoized function should not have side effects._  
Memoization will prevent the function from executing if the inputs are the same therefore if you depend on some side effects resulting from the function execution you will lose them.  

_A memoized function should be pure._  
That means that the function result is the same if the inputs are the same, if the function was to depend on other context it would be impossible to memoize correctly as only parameters can be cached. 

# Limitations

The memoization mechanism does not event try to be thread safe, like, at all. Keep that in mind.  

Only the _last_ parameters used to invoke the function are recorded, if they change the function is evaluated again.  
This is to make sure that the memoization does not use too much memory and that there is a reliable cache invalidation.  
Therefore it is better if you memoize only functions that you know are called many times with the same parameters.  
