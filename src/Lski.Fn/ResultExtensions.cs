using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
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
    }
}