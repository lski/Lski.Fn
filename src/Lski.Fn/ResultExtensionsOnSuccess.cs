using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            return result.IsSuccess ? func() : Result.Fail<T>(result.Error);
        }

        /// <summary>
        /// Runs passed function and returns a new Result, otherwise returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnSuccess<T>(this Result result, Func<Result> func)
        {
            return result.IsSuccess ? func() : Result.Fail(result.Error);
        }

        /// <summary>
        /// Runs passed function and returns the original Result, otherwise returns a new Failed Result
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }
            action();
            return result;
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, Result<T2>> func)
        {
            return result.IsSuccess ? func(result.Value) : Result.Fail<T2>(result.Error);
        }

        /// <summary>
        /// Runs passed function and returns a new Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, T2> func)
        {
            return result.IsSuccess ? Result.Ok(func(result.Value)) : Result.Fail<T2>(result.Error);
        }

        /// <summary>
        /// Runs passed function and returns the original Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }
            action();
            return result;
        }

        /// <summary>
        /// Runs passed function and returns the original Result&lt;T&gt;, otherwise returns a new Failed Result&lt;T&gt;
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }
            action(result.Value);
            return result;
        }
    }
}