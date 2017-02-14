using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        [DebuggerStepThrough]
        public static Result<T> OnBoth<T>(this Result result, Func<Result, Result<T>> func) => func(result);

        [DebuggerStepThrough]
        public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

        [DebuggerStepThrough]
        public static Result<T2> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Result<T2>> func) => func(result);

        [DebuggerStepThrough]
        public static T2 OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, T2> func) => func(result);

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnBoth<T>(this Task<Result> task, Func<Result, Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        [DebuggerStepThrough]
        public static async Task<T> OnBoth<T>(this Task<Result> task, Func<Result, T> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T2>> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, Result<T2>> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }

        [DebuggerStepThrough]
        public static async Task<T2> OnBoth<T1, T2>(this Task<Result<T1>> task, Func<Result<T1>, T2> func)
        {
            var result = await task.ConfigureAwait(false);
            return func(result);
        }
    }
}