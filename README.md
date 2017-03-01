# Lski.Fn

[![Nuget downloads](https://img.shields.io/nuget/v/lski.fn.svg)](https://www.nuget.org/packages/Lski.Fn/)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/lski/Lski.Fn/blob/master/LICENSE)

A few simple functional class; Result, Maybe and Either to enable a more functional style of programming in C#. Although written from scratch it is heavily influenced by a couple of others projects, please see the [references](#references) below.

## Result

At its simplest a Result can be used to wrap a response from an action that could potentially not succeed. If an action works as desired it can be described as the "happy path" and it can be trickey to handle the "none happy path" where things go wrong, without throwing expensive exceptions. I highly recommend the [Railway Oriented Programming](https://vimeo.com/97344498) video which explains it in better detail.

Returning a result object that states whether an action was successful or not and contains either the data from the action or the error that was encountered as a message. 

Create a Result:
```csharp
// All the following create an equivalent object, choose the style right for the situation.

var result1 = "success".ToSuccess();  
// or directly
var result2 = Result.Success("success");
// or implicitly casted
Result<string> result3 = "success";

// result1 == result2 == result3

// Create a failed result
var result4 = "an error occured".ToFail<string>(); 
// or directly
var result5 = Result.Fail<string>("An error occured");

// result4 == result5
``` 

Use a Result:
```csharp
// explicitly
var result = "success".ToSuccess();  
if (result.IsSuccess) {
    var val = result.Value; // If IsSuccess, result.Value can not be null
}

var result = Result.Fail<string>("An error occured");
if (result.IsFailure) {
    var err = result.Error;
}

// or fluently 
"success".ToSuccess()
    .OnSuccess((val) => {
        // Do stuff only if successful
    });

// result is a failure
"an error occured".ToFail<string>()
    .OnFailure((err) => {
        // Do Stuff only if a failure
    });
```

__NB:__ A successful Result can not contain a null value, so you can be certain that a value will not need to be checked for null.

Chain together:
```csharp
"success".ToSuccess()
    .OnFailure((err) => {
        // NOT run
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

You could ask whats the advantage to the above, but in large quanities of code this can make it easier to read, but also means that it encourages small pure functions that can be more easily tested. Also smaller blocks of code are generally easier to maintain as the complexity is lower.

## Maybe

Generally, although debated, using null is not a recommended practice, it can be an anti pattern as using it has many drawbacks, see [references](#references). One of my least favourite is it can introduce ambiguity in code, say a method doesn't state in the name it can/cant be null then is will mean a developer using that method will need to constantly check if an object being returned is null or not.

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

So we can use Maybe<T> as a way of wrapping a potentially null value. Using Maybe<T> does two things, it makes it obvious to the subscriber of the variable that the value can be empty, but also it means type checking with "is" works as expected.

Create a Maybe object:
```csharp
var myVar1 = "a value".Maybe();
// or
var myVar2 = Maybe.Create("a value");
// implicit casting
Maybe<string> myVar3 = "a value";

// myVar1 == myVar2 == myVar3
``` 

Create a 'null' Maybe:
```csharp
var myVar1 = Maybe.None<string>();
// or 
var myVar2 = Maybe.Create<string>(null);

// mayVar1 == myVar2
```

Then use HasValue to test for a null value
```csharp
if (myVar.HasValue) { // or mrVar.HasNoValue
    otherVar = myVar.Value;
}
```

Or use a lamda:
```csharp
myVar.Bind((val) => {
    // Only runs if myVar has a value
});
```

Access the underlying value, providing a default value:
```csharp
var maybe = Maybe<string>.None();
var val = maybe.Unwrap("a default value");
```

Type checking is also possible on what would have been a null value:
```csharp
var bar = Maybe<string>(null);
var isNullMaybe = bar is Maybe<string>;
// isNullMaybe == true
```

__*NB*__ The Maybe pattern is sometimes called "Option" as it is F#

## Either

Either<TLeft, TRight> can be thought of as an intelligent Tuple, but just storing a single value. Useful when you need to return either one of two values (types) from a function and give type safety.

Either also provides the ability to handle different execution paths.

```csharp
var either = Either.Left<string, int>("left");
// or implicitly
Either.Left<string, int> either = "left";
```

Usage:
```csharp
if (either.IsLeft) {
    string home = either.Left();
}
```

If wanting to handle the possibility of either side
```csharp
either.LeftOrRight((left) => {
        // Runs as it is a left-sided either
    }),
    (right) => {
        // Does not run
    });
```

### References
- [Railway Oriented Programming](https://vimeo.com/97344498) 2 Years old but still a very relevant and a great watch.
- [Functional C#: Handling failures, input errors](http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/)
- [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions) by [Vladimir Khorikov](https://github.com/vkhorikov). This project is heavily influenced by that project and has a similar API footprint.
- [The Definitive Reference To Why Maybe Is Better Than Null](http://www.nickknowlson.com/blog/2013/04/16/why-maybe-is-better-than-null/)
- [Null has no type, but Maybe has](http://blog.ploeh.dk/2015/11/13/null-has-no-type-but-maybe-has/)
- [Why Null is bad](http://www.yegor256.com/2014/05/13/why-null-is-bad.html)
