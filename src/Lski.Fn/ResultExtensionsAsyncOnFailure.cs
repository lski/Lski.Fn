using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    public static partial class ResultExtensionsAsync
    {
        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Result> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, Error> func)
        {
            var result = await task.ConfigureAwait(false);

            return result.IsFailure ? Result.Fail(func(result.Error)) : result;
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Func<Error, string> func)
        {
            var result = await task.ConfigureAwait(false);

            return result.IsFailure ? Result.Fail(func(result.Error)) : result;
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action<Error> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailure(this Task<Result> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Result<T>> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, Error> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Func<Error, string> func)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(func);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action<string> action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and the original result is returned.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> task, Action action)
        {
            var result = await task.ConfigureAwait(false);
            return result.OnFailure(action);
        }

        /// <summary>
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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
        /// If an unsuccessful result (a failure) the function is run and a new Result returned. Returns the original result if successful.
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

        /// <summary>
        /// If a failure result throws the exception created by the func
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> OnFailThrow(this Task<Result> task, Func<Error, Exception> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result;
            }

            throw func(result.Error);
        }

        /// <summary>
        /// If a failure result throws the exception created by the func
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> OnFailThrow<T>(this Task<Result<T>> task, Func<Error, Exception> func)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result;
            }

            throw func(result.Error);
        }
    }
}