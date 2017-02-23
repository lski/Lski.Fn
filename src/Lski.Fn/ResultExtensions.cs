using System.Diagnostics;
using System.Threading.Tasks;

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
        public static Result<T> ToResult<T>(this T data) => Result.Ok(data);

        /// <summary>
        /// Turn a variable into a successful Result of the same type
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> ToResult<T>(this Task<T> task)
        {
            var result = await task.ConfigureAwait(false);
            return Result.Ok(result);
        }

        /// <summary>
        /// Turn a variable into a successful Result of the same type
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> ToSuccess<T>(this T data) => Result.Ok(data);

        /// <summary>
        /// Turn a variable into a successful Result of the same type
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> ToSuccess<T>(this Task<T> task)
        {
            var result = await task.ConfigureAwait(false);
            return Result.Ok(result);
        }

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
        public async static Task<Result> ToFail(this Task<Error> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail(error);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> ToFail<T>(this Task<Error> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail<T>(error);
        }

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
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result> ToFail(this Task<string> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail(error);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> ToFail<T>(this Task<string> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail<T>(error);
        }
    }
}