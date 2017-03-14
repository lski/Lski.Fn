using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    /// <summary>
    /// Contains async extension pass-thru functions for Result and Result&lt;T&gt; objects
    /// </summary>
    public static partial class ResultExtensionsAsync
    {
        /// <summary>
        /// Turn a variable into a successful Result of the same type
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> ToSuccess<T>(this Task<T> task)
        {
            var result = await task.ConfigureAwait(false);
            return Result.Success(result);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result> ToFail(this Task<Error> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail(error);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> ToFail<T>(this Task<Error> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail<T>(error);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result> ToFail(this Task<string> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail(error);
        }

        /// <summary>
        /// Turn an error message to a failed result
        /// </summary>
        [DebuggerStepThrough]
        public async static Task<Result<T>> ToFail<T>(this Task<string> task)
        {
            var error = await task.ConfigureAwait(false);
            return Result.Fail<T>(error);
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> Ensure(this Task<Result> task, Func<bool> check, Error error)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result.Error.ToFail();
            }

            return check() ? Result.Success() : error.ToFail();
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> task, Func<T, bool> check, Error error)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result.Error.ToFail<T>();
            }

            return check(result.Value) ? result.Value.ToSuccess() : error.ToFail<T>();
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> Ensure(this Result result, Func<Task<bool>> check, Error error)
        {
            if (result.IsFailure)
            {
                return result.Error.ToFail();
            }

            var passed = await check().ConfigureAwait(false);

            return passed ? Result.Success() : Result.Fail(error);
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> Ensure<T>(this Result<T> result, Func<T, Task<bool>> check, Error error)
        {
            if (result.IsFailure)
            {
                return result.Error.ToFail<T>();
            }

            var passed = await check(result.Value).ConfigureAwait(false);

            return passed ? Result.Success(result.Value) : Result.Fail<T>(error);
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result> Ensure(this Task<Result> task, Func<Task<bool>> check, Error error)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result.Error.ToFail();
            }

            var passed = await check().ConfigureAwait(false);

            return passed ? Result.Success() : Result.Fail(error);
        }

        /// <summary>
        /// Runs the check function if result is a success. If check returns false the a Failed result is returned with the error message
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> task, Func<T, Task<bool>> check, Error error)
        {
            var result = await task.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result.Error.ToFail<T>();
            }

            var passed = await check(result.Value).ConfigureAwait(false);

            return passed ? Result.Success(result.Value) : Result.Fail<T>(error);
        }
    }
}