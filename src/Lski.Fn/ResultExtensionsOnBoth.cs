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
        public static Result<T2> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Result<T2>> func) => func(result);

        /// <summary>
        /// Runs the function and provides the result object and returns the response of the function call
        /// </summary>
        [DebuggerStepThrough]
        public static T2 OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, T2> func) => func(result);
    }
}