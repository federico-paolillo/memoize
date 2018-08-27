[![MIT License](https://img.shields.io/github/license/federico-paolillo/memoize.svg?style=flat-square)](https://github.com/federico-paolillo/memoize/blob/master/LICENSE)
[![Travis branch](https://img.shields.io/travis/federico-paolillo/memoize/master.svg?style=flat-square)](https://travis-ci.org/federico-paolillo/memoize)
[![NuGet](https://img.shields.io/nuget/v/FP.Memoization.svg?style=flat-square)](https://www.nuget.org/packages/FP.Memoization/)

# Memoizer

Memoize C# functions with ease.  
No dependencies, targets [.NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md).  
All 16 Func delegates are supported and tested using source code generation.

# Why ?

[From Wikipedia:](https://en.wikipedia.org/wiki/Memoization)  
_In computing, memoization [...] is an optimization technique used primarily to speed up computer programs by storing the results of expensive function calls and returning the cached result when the same inputs occur again._

Essentially we want to avoid executing a function again if its inputs do not change.  
This repository provides helper methods to memoize a function that can take up to 16 parameters.

Another reason for this package is that most of NuGet packages out there are outdated and do not target .NET Standard so they cannot be used with .NET Core.  

# How do I use it ?

Add an `using` for `Memoization` namespace.  
Call `Memoizer.Memoize(fn)` and pass it the function that you want to memoize.  
You will get back and instance of `Memoizer` which you can use to control memoization for your function.  
Invoke the method `Call` from the `Memoizer` instance to execute the memoized function.  

For example:  

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

A `Memoizer` instance can also be implicitly converted to a Func so you can use it more transparently (e.g.: with events, callbacks or whatever want a `Func` delegate).  
What you get is essentially a reference to `Memoizer.Call` that you can still control from the `Memoizer` instance.  

```csharp

//Memoize the function
var memoizedFn = Memoizer.Memoize(SomeFunctionThatYouWantToMemoize);

//Convert the Memoizer instance to a `Func` implictily
Func<string, string> memoizedFnAsFunc = memoizedFn;

```

Another thing to take into consideration is that **memoization might generate memory leaks** because the lifetime of the parameters will be as long as the memoized function instance (because parameters are stored in the memoized function instance).  

In case you want to clear any memoized parameters and results you can call `Memoizer.Reset` on a `Memoizer` instance, like so:  

```csharp

//Memoize the function
var memoizedFn = Memoizer.Memoize(SomeFunctionThatYouWantToMemoize);

//Cleanup any memoized data
memoizedFn.Reset();

```

This is useful if you wish to force evaluating the function again or clear any parameters and results stored.  
Note that any custom `IEqualityComparer<T>` registered will **not** be removed.

You can also influence how some parameter types are compared by suppling a custom `IEqualityComparer<T>` for the type you want to compare using `Memoizer.WithEqualityComparer<T>`.  

```csharp

//Memoize the function
var memoizedFn = Memoizer.Memoize(SomeFunctionThatYouWantToMemoize);

memoizedFn.WithEqualityComparer<T>(StringComparer.OrdinalIgnoreCase);

```

There aren't many safety checks in place, so you can supply as many `IEqualityComparer<T>` as you want, even for types that do not appear in the function signature.  
If you supply different `IEqualityComparer<T>` for the same type only the last one will be stored and used.  

Note that this implementation does not attempt to take into consideration subclasses or interfaces, so if your type implements an interface and you supply an `IEqualityComparer<T>` for that interface it will not be taken into consideration during comparison. You must supply comparers for the _exact_ type that appears in the function signature.    

# Tips on function memoization

_Memoization works best if the parameters are immutable_.  
Having immutable parameters allows us to compare them with a simple reference equal, improving performance.

_A memoized function should not have side effects._  
Memoization will prevent the function from executing if the inputs are the same therefore if you depend on some side effects resulting from the function execution you will lose them.  

_A memoized function should be pure._  
That means that the function result is the same if the inputs are the same, if the function was to depend on other context it would be impossible to memoize correctly as only parameters can be cached. 

# Limitations

_The memoization mechanism does not even try to be thread safe_. Keep that in mind.  

To make sure that the memoization implementation does not use too much memory and that there is a reliable cache invalidation mechanism only the _last_ parameters used to invoke the function are recorded, if they change the function is evaluated again. Therefore it is better if you memoize only functions that you know are called many times with the same parameters.  
