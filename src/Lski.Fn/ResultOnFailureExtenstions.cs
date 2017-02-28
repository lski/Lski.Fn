using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Func<Error, Result> func)
        {
            return result.IsFailure ? func(result.Error) : result;
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the action is run and the original result is returned.
        /// </summary>
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

        /// <summary>
        /// If a result is unsuccessful (a failure) the action is run and the original result is returned.
        /// </summary>
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

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Result<T>> func)
        {
            return result.IsFailure ? func(result.Error) : result;
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Error> func)
        {
            return result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, string> func)
        {
            return result.IsFailure ? Result.Fail<T>(func(result.Error)) : result;
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
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

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
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

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Result> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Error> func)
        {
            var result = await task.ConfigureAwait(false);
            var err = func(result.Error);
            return Result.Fail(err);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, string> func)
        {
            var result = await task.ConfigureAwait(false);
            var err = func(result.Error);
            return Result.Fail(err);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action<Error> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Error> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, string> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action<string> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Result result, Func<Error, Task<Result>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Error, Task<Result<T>>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Error, Task<Error>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var err = await func(result.Error).ConfigureAwait(false);

            return Result.Fail<T>(err);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Result<T> result, Func<Error, Task<string>> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var err = await func(result.Error).ConfigureAwait(false);

            return Result.Fail<T>(err);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Task<Result>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Task<Result<T>>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            return await func(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Task<Error>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            var err = await func(result.Error).ConfigureAwait(false);

            return Result.Fail<T>(err);
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Task<string>> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result;
            }

            var err = await func(result.Error).ConfigureAwait(false);

            return Result.Fail<T>(err);
        }
    }
}