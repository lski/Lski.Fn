using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensionsAsync
    {
        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> task, Func<Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        /// <summary>
        /// Runs passed function and returns a new Result, otherwise returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess<T>(this Task<Result> task, Func<Result> func)
        {
            var result = await task.ConfigureAwait(false);
            // TODO 
            return result.IsSuccess ? func() : Result.Fail(result.Error);
        }

        /// <summary>
        /// Runs passed function and returns the original Result, otherwise returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess(this Task<Result> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Task<Result<T1>> task, Func<T1, Result<T2>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Task<Result<T1>> task, Func<T1, T2> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> task, Action<T> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Result result, Func<Task<Result<T>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            return await func().ConfigureAwait(false);
        }

        /// <summary>
        /// Runs passed function and returns the original Result, otherwise returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess<T>(this Result result, Func<Task<Result>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            return await func().ConfigureAwait(false);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, Task<Result<T2>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T2>(result.Error);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, Task<T2>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T2>(result.Error);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return value.ToSuccess();
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> task, Func<Task<Result<T>>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await result.OnSuccess(func);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess<T>(this Task<Result> task, Func<Task<Result>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            return await func();
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Task<Result<T1>> task, Func<T1, Task<Result<T2>>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T2>(result.Error);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnSuccess<T1, T2>(this Task<Result<T1>> task, Func<T1, Task<T2>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<T2>(result.Error);
            }

            return await func(result.Value).ToSuccess();
        }
    }
}