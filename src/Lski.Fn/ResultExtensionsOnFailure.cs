using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Func<Error, Error> func)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            return Result.Fail(func(result.Error));
        }

        /// <summary>
        /// If a result is unsuccessful (a failure) the function is run and a new Result returned. Returns the original result if successful.
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Func<Error, string> func)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            return Result.Fail(func(result.Error));
        }

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
        /// If a failure result throws the exception created by the func
        /// </summary>
        [DebuggerStepThrough]
        public static Result OnFailThrow(this Result result, Func<Error, Exception> func)
        {
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
        public static Result<T> OnFailThrow<T>(this Result<T> result, Func<Error, Exception> func)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            throw func(result.Error);
        }
    }
}