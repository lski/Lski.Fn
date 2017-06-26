# Lski.Fn

[![Nuget downloads](https://img.shields.io/nuget/v/lski.fn.svg)](https://www.nuget.org/packages/Lski.Fn/)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/lski/Lski.Fn/blob/master/LICENSE)

A few simple functional class; Result, Maybe and Either to enable a more functional style of programming in C#. Although written from scratch it is heavily influenced by a couple of others projects, please see the [references](#references) below.

## Semantic Versioning

This project adheres to semantic versioning, so any breaking changes will be restricted to major version increases.

## Result

At its simplest a Result can be used to wrap a response from an action that could potentially not succeed. If an action works as desired it can be described as the "happy path" and it can be trickey to handle the "none happy path" where things go wrong, without throwing expensive exceptions. I highly recommend the [Railway Oriented Programming](https://vimeo.com/97344498) video which explains it in better detail.

Returning a result object that states whether an action was successful or not and contains either the data from the action or the error that was encountered as a message. 

Create a Successful Result:

```csharp
// explicitly
var foo = new Bar();
var result = foo.ToSuccess(); // == Result.Success("success");

// implicitly
public Result<Bar> GetString() 
{
    return new Bar();
}
```

Create a Failed Result:
```csharp
// explicity
var result = "an error occured".ToFail<Bar>(); // == Result.Fail<string>("An error occured");

// implicitly
public Result<Bar> GetString() 
{
    return new Error("an error occured");
}
```

Use a Result:
```csharp
// directly
if (result.IsSuccess) { // or result.IsFailure
    var val = result.Value;
}
else {
    var err = result.Error; 
}

// or fluently 
result.OnSuccess((val) => {
    // Do stuff only if successful
});

result.OnFailure((err) => {
    // Do stuff only if a failure
});
```

__NB:__ A successful Result can not contain a null value, so you can be certain that a value will not need to be checked for null.

Chain together:
```csharp
result
    .OnFailure((err) => {
        // Not run
    })
    .OnSuccess((val) => {
        // Do stuff and return a new result
        return DateTime.Now;
    })
    .OnSuccess((val) => {
        // val is now of the return type from the OnSuccess above so a DateTime object
        val.AddDays(1);
    });
```

Whats the advantage to the above? In large quanities of code this can make it easier to read, but also means that it encourages small pure functions that can be more easily tested. Also smaller blocks of code are generally easier to maintain as the complexity is lower.

## Maybe

Although debated, using null is not a recommended practice as it can be an anti pattern and using it has many drawbacks, see [references](#references).

For me the biggest pain is because we can never be sure that a variable is null or not we have to constantly check for a null 'value' as it can be amibigous.

```csharp
string name = anObj.GetName();
if (name != null) {
    // This is the only way to be sure if the value has been set. 
    // And needs to be everywhere the method is used.
}
```

Another problem can be type checking, if a value is null then checking it with the operator `is` probably doesnt work as you would expect it too.

```csharp
string foo = null;
var isNullAString = foo is string;

// isNullAString == false
```

So we can use Maybe<T> as a way of wrapping a potentially null value. 

Using Maybe<T> does two things, it makes it obvious to the subscriber of the variable that the value can be empty, but also it means type checking with "is" works as expected.

Create a Maybe object:
```csharp
var myVar = "a value".ToMaybe(); // == Maybe.Create("a value");

// implicit casting
public Maybe<string> GetString() 
{
    return "Hello World";
}
``` 

Create a 'null' Maybe:
```csharp
var myVar = Maybe.None<string>(); // == Maybe.Create<string>(null);
```

Then use HasValue to test for a null value
```csharp
// directly
if (myVar.HasValue) { // or myVar.HasNoValue
    otherVar = myVar.Value; // Accessing Value when it hasnt one results in an InvalidOperationException
}

// fluently
myVar.Bind((val) => {
    // Only runs if myVar has a value, val is never null
});
```

Access the underlying value, providing a default value if null:
```csharp
var val = maybe.Unwrap("a default value");
```

Type checking is also possible on what would have been a null value:
```csharp
var bar = Maybe<string>(null);
var isNullMaybe = bar is Maybe<string>;

// isNullMaybe == true
```

__*NB*__ The Maybe pattern is sometimes called "Option" as it is in F#

## Either

Either<TLeft, TRight> is a wrapper class where you are unsure if an action will give one answer or another, it could be thought of as a more generic version of Result<T>.

Useful when you need to return either one of two values from a function and give type safety. Either also provides the ability to handle different execution paths.

```csharp
// explicitly
var either = "Hello World".ToLeft<string, int>(); // == Either.Left<string, int>("Hello World");

// implicitly
public Either<string, int> GetValue() 
{
    return "Hello World";
}
```

Usage:
```csharp
if (either.IsLeft) {
    // Would throw an exception if not a left-sided, hence the IsLeft check.
    string left = either.ToLeft();
}

// does not throw an exception, but returns the default value if incorrect side
string left = either.ToLeft("a default value"); 
```

Chaining:

```csharp
var either = Either.Left<int, string>(100);

either = either
    .Left(val => val + 11);             // runs and returns a new either
    .Right(right => val + "not run")    // doesnt run
    .Left(left => val + " dalmatians"); // runs and returns a new right-sided either

either.ToRight() == "101 dalmatians"    // true
```

If wanting to handle the possibility of either side at the same time:
```csharp
var either = Either.Left<int, string>(0);
    
either.LeftOrRight(left => /* Runs */ 10, right => /* Does not run */ "foo");
```

Returning a resolved value from either side:
```csharp
var result = either.ToValue(left => left += "world", right => right += " == 10");
```

As both functions return the same type, in this case a string, the correct func, based on the side of the either, runs and returns a value. A useful way of breaking out of an either to give a resolved value.

### References
- [Railway Oriented Programming](https://vimeo.com/97344498) 2 Years old but still a very relevant and a great watch.
- [Functional C#: Handling failures, input errors](http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/)
- [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions) by [Vladimir Khorikov](https://github.com/vkhorikov). This project is heavily influenced by that project
- [The Definitive Reference To Why Maybe Is Better Than Null](http://www.nickknowlson.com/blog/2013/04/16/why-maybe-is-better-than-null/)
- [Null has no type, but Maybe has](http://blog.ploeh.dk/2015/11/13/null-has-no-type-but-maybe-has/)
- [Why Null is bad](http://www.yegor256.com/2014/05/13/why-null-is-bad.html)
