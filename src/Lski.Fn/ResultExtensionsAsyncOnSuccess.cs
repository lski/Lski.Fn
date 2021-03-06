﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensionsAsync
    {
        /// <summary>
        /// If a successful result runs passed function and returns a new Result, otherwise function doesnt run and returns the Failed Result
        /// </summary>
        [DebuggerStepThroughAttribute]
        public static async Task<Result> OnSuccess<TIn>(this Task<Result<TIn>> task, Func<TIn, Task<Result>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result, otherwise function doesnt run and returns the Failed Result
        /// </summary>
        [DebuggerStepThroughAttribute]
        public static async Task<Result> OnSuccess<T>(this Task<Result<T>> task, Func<T, Result> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            return func(result.Value);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result, otherwise function doesnt run and returns the Failed Result
        /// </summary>
        [DebuggerStepThroughAttribute]
        public static async Task<Result> OnSuccess<T>(this Result<T> result, Func<T, Task<Result>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> task, Func<Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);

            return result.OnSuccess(func);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result, otherwise function doesnt run and returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess<T>(this Task<Result> task, Func<Result> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.IsSuccess ? func() : Result.Fail(result.Error);
        }

        /// <summary>
        /// If a successful result runs passed function and returns the original Result, otherwise action doesnt run and returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess(this Task<Result> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, Result<TOut>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, TOut> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(func);
        }

        /// <summary>
        /// If a successful result runs passed action and returns a new Result&lt;T&gt;, otherwise action doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// If a successful result runs passed action and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> task, Action<T> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnSuccess(action);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
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
        /// If a successful result runs passed function and returns the original Result, otherwise function doesnt run and returns a new Failed Result
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
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            var value = await func(result.Value).ConfigureAwait(false);

            return value.ToSuccess();
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> task, Func<Task<Result<T>>> func)
        {
            var result = await task.ConfigureAwait(false);
            return await result.OnSuccess(func);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnSuccess(this Task<Result> task, Func<Task<Result>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            return await func();
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, Task<Result<TOut>>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return await func(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If a successful result runs passed function and returns a new Result&lt;T&gt;, otherwise function doesnt run and returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<TOut>> OnSuccess<TIn, TOut>(this Task<Result<TIn>> task, Func<TIn, Task<TOut>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return await func(result.Value).ToSuccess();
        }
    }
}