using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Func<Error, Result> func) => result.IsFailure ? func(result.Error) : result;

        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Action<Error> action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action(result.Error);
            return result;
        }

        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action();
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Result<T>> func) => result.IsFailure ? func(result.Error) : result;

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Error> func) => result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, string> func) => result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action(result.Error);
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action();
            return result;
        }

        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Result> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action<Error> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Error> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, string> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action<string> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }
    }
}