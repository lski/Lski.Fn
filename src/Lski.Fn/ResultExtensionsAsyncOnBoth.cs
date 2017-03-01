using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensionsAsync
    {
        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnBoth<T>(this Task<Result> task, Func<Result, Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> OnBoth<T>(this Task<Result> task, Func<Result, T> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, Result<T2>> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T2> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, T2> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnBoth<T>(this Result result, Func<Result, Task<Result<T>>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> OnBoth<T>(this Result result, Func<Result, Task<T>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Task<Result<T2>>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T2> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Task<T2>> func)
        {
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnBoth<T>(this Task<Result> task, Func<Result, Task<Result<T>>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> OnBoth<T>(this Task<Result> task, Func<Result, Task<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, Task<Result<T2>>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T2> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, Task<T2>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await func(result).ConfigureAwait(false);
        }
    }
}