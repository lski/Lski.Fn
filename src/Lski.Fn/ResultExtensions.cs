using System.Diagnostics;

namespace Lski.Fn
{
    /// <summary>
    /// Additional functions for Result and Result&lt;T&gt; objects
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Turn a variable into a successful Result of the same type
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> ToSuccess<T>(this T data) => Result.Ok(data);

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public static Result ToFail(this Error message) => Result.Fail(message);

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> ToFail<T>(this Error message) => Result.Fail<T>(message);


        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public static Result ToFail(this string message) => Result.Fail(message);

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> ToFail<T>(this string message) => Result.Fail<T>(message);

// #region OnSuccess

//         /// <summary>
//         /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
//         {
//             return result.IsSuccess ? func() : Result.Fail<T>(result.Error);
//         }

//         /// <summary>
//         /// Runs passed function and returns a new Result, otherwise returns a new Failed Result
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result OnSuccess<T>(this Result result, Func<Result> func)
//         {
//             return result.IsSuccess ? func() : Result.Fail(result.Error);
//         }

//         /// <summary>
//         /// Runs passed function and returns the original Result, otherwise returns a new Failed Result
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result OnSuccess(this Result result, Action action)
//         {
//             if (result.IsFailure)
//             {
//                 return Result.Fail(result.Error);
//             }
//             action();
//             return result;
//         }

//         /// <summary>
//         /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, Result<T2>> func)
//         {
//             return result.IsSuccess ? func(result.Value) : Result.Fail<T2>(result.Error);
//         }

//         /// <summary>
//         /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, T2> func)
//         {
//             return result.IsSuccess ? Result.Ok(func(result.Value)) : Result.Fail<T2>(result.Error);
//         }

//         /// <summary>
//         /// Runs passed function and returns the original Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
//         {
//             if (result.IsFailure)
//             {
//                 return Result.Fail<T>(result.Error);
//             }
//             action();
//             return result;
//         }

//         /// <summary>
//         /// Runs passed function and returns the original Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
//         {
//             if (result.IsFailure)
//             {
//                 return Result.Fail<T>(result.Error);
//             }
//             action(result.Value);
//             return result;
//         }

// #endregion OnSuccess

// #region OnFailure

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result OnFailure(this Result result, Func<Error, Result> func)
//         {
//             return result.IsFailure ? func(result.Error) : result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the action is run and the original result is returned.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result OnFailure(this Result result, Action<Error> action)
//         {
//             if (result.IsSuccess)
//             {
//                 return result;
//             }
//             action(result.Error);
//             return result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the action is run and the original result is returned.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result OnFailure(this Result result, Action action)
//         {
//             if (result.IsSuccess)
//             {
//                 return result;
//             }
//             action();
//             return result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Result<T>> func)
//         {
//             return result.IsFailure ? func(result.Error) : result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Error> func)
//         {
//             return result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, string> func)
//         {
//             return result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
//         {
//             if (result.IsSuccess)
//             {
//                 return result;
//             }
//             action(result.Error);
//             return result;
//         }

//         /// <summary>
//         /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnFailure<T>(this Result<T> result, Action action)
//         {
//             if (result.IsSuccess)
//             {
//                 return result;
//             }
//             action();
//             return result;
//         }

// #endregion OnFailure

// #region OnBoth

//         /// <summary>
//         /// Runs the function and provides the result object and returns a new Result.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T> OnBoth<T>(this Result result, Func<Result, Result<T>> func) => func(result);

//         /// <summary>
//         /// Runs the function and provides the result object and returns the response of the function call
//         /// </summary>
//         [DebuggerStepThrough]
//         public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

//         /// <summary>
//         /// Runs the function and provides the result object and returns a new Result.
//         /// </summary>
//         [DebuggerStepThrough]
//         public static Result<T2> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Result<T2>> func) => func(result);

//         /// <summary>
//         /// Runs the function and provides the result object and returns the response of the function call
//         /// </summary>
//         [DebuggerStepThrough]
//         public static T2 OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, T2> func) => func(result);

// #endregion OnBoth

    }
}