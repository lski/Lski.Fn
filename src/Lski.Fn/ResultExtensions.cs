using System;
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
        public static Result<T> ToSuccess<T>(this T data) => Result.Success(data);

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

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static Result Ensure(this Result result, Func<bool> check, Error error)
        {
            if (result.IsFailure)
            {
                return result.Error.ToFail();
            }

            return check() ? Result.Success() : error.ToFail();
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> check, Error error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return check(result.Value) ? result.Value.ToSuccess() : error.ToFail<T>();
        }
    }
}