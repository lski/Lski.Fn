# Lski.Fn

[![Nuget downloads](https://img.shields.io/nuget/v/lski.fn.svg)](https://www.nuget.org/packages/Lski.Fn/)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/lski/Lski.Fn/blob/master/LICENSE)

A few simple functional class; Result, Maybe and Either to enable a more functional style of programming in C#. 

## Result

Used to handle the "none happy path" basically allowing a developer to handle when an expected error happens and you want to avoid throwing expensive exceptions. I highly recommend the [Railway Oriented Programming](https://vimeo.com/97344498) video which explains it in better detail.

Create a Result:
```csharp
var result = "success".ToSuccess();  
// or directly
var result = Result.Ok("success");
// or implicitly converted
Result<string> result = "success";

// NB: All create an equivalent Result containing the same value, choose a style right for the situation.
``` 

Use a Result:
```csharp
// explicitly
var result = Result.Ok("success");  
if (result.IsSuccess) {
    // NB: If IsSuccess is true, result.Value can not be null (Use Maybe below if nulls are required)
    var val = result.Value;
}

// or fluently 
Result.Ok("success")
    .OnSuccess((val) => {
        // Do stuff only if successful
    });
``` 

If a result is a failure:
```csharp
Result.Fail<string>("An error occured")
    .OnFailure((err) => {
        // Do Stuff only if a failure
    });
```

Chain together:
```csharp
Result.Ok("success").
    .OnFailure((err) => {
        // NOT run
    })
    .OnSuccess((val) => {
        // Do stuff and return a new result
    })
    .OnSuccess((val) => {
        // Do stuff on the new result from the above OnSuccess
    });
```

You could ask whats the advantage to the above, but in large quanities of code this can make it easier to read, but also it makes passing results of an operation around much easier.

## Maybe

Generally, although debated, using null is not a recommended practice (see links in references), we can use Maybe<T> as a way of wrapping a potentially null value. Using Maybe<T> does two things, it makes it obvious to the subscriber of the variable that the value can be empty, but also it means type checking with "is" works as expected.

```csharp
var myVar = "a value".Maybe();
// or
var myVar = Maybe.Create("another value");
var myVar = Maybe<string>.None();
``` 

Then use HasValue to test for a null value
```csharp
if (myVar.HasValue) {
    otherVar = myVar.Value;
}
// or mrVar.HasNoValue
```

Or use a lamda:
```csharp
myVar.Do((val) => {
    // We know for certain val is not null and only runs if the maybe contains a value
});
```

Type checking:
```csharp
string foo = null;
var isNullAString = foo is string;
// isNullAString == false

var bar = Maybe<string>(null);
var isNullMaybe = bar is Maybe<string>;
// isNullMaybe == true
```

Access the underlying value, providing a default value
```csharp
var maybe = Maybe<string>.None();
var val = maybe.Unwrap("a default value");
```

__*NB*__ The Maybe pattern is sometimes called "Option" and is in F#

## Either

Either<TFirst, TSecond> can be thought of as an intelligent Tuple, but just storing a single value. Useful when you need to return either one of two valeus (types) from a function. 

Either also provides the ability to handle different execution paths.

```csharp
var either = Either.First<string, int>("left");
// or implicitly (left and right types need to be different for this to work)
Either.First<string, int> either = "left";

either.Do((val) = {
    // Runs as the first was chosen is valid
}),
(val) => {
    // Does not run
});
```

Alternatively you can be selective which side you run.
```csharp
either.Left((val) => Console.WriteLine(val));
```

### References
- [Railway Oriented Programming](https://vimeo.com/97344498) 2 Years old but still a very relevant and a great watch.
- [Functional C#: Handling failures, input errors](http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/)
- [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions) by [Vladimir Khorikov](https://github.com/vkhorikov). This project is heavily influenced by that project and has a similar API footprint.
- [The Definitive Reference To Why Maybe Is Better Than Null](http://www.nickknowlson.com/blog/2013/04/16/why-maybe-is-better-than-null/)
- [Null has no type, but Maybe has](http://blog.ploeh.dk/2015/11/13/null-has-no-type-but-maybe-has/)
