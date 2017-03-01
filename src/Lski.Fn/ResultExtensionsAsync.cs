using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    /// <summary>
    /// Contains async extension pass-thru functions for Result and Result&lt;T&gt; objects
    /// </summary>
    public static partial class ResultExtensionsAsync
    {
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