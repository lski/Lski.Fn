using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnBoth<T>(this Result result, Func<Result, Result<T>> func) => func(result);

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

        /// <summary>
        /// Runs the function and provides the result object and returns a new Result.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<TOut> OnBoth<TIn, TOut>(this Result<TIn> result, Func<Result<TIn>, Result<TOut>> func) => func(result);

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static TOut OnBoth<TIn, TOut>(this Result<TIn> result, Func<Result<TIn>, TOut> func) => func(result);
    }
}