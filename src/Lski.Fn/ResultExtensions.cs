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
    }
}