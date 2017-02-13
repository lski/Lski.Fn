using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public static class ResultExtensions
    {
        [DebuggerStepThrough]
        public static Result<T> ToResult<T>(this T data) => Result.Ok(data);

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func) => result.IsSuccess ? func() : Result.Fail<T>(result.Error);

        public static Result OnSuccess<T>(this Result result, Func<Result> func) => result.IsSuccess ? func() : Result.Fail(result.Error);

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }
            action();
            return result;
        }

        public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, Result<T2>> func) => result.IsSuccess ? func(result.Value) : Result.Fail<T2>(result.Error);

        public static Result<T2> OnSuccess<T1, T2>(this Result<T1> result, Func<T1, T2> func) => result.IsSuccess ? Result.Ok(func(result.Value)) : Result.Fail<T2>(result.Error);

        public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }
            action();
            return result;
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }
            action(result.Value);
            return result;
        }

        public static Result OnFailure(this Result result, Func<Error, Result> func) => result.IsFailure ? func(result.Error) : result;

        public static Result OnFailure(this Result result, Action<Error> action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action(result.Error);
            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action();
            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Func<Error, Result<T>> func) => result.IsFailure ? func(result.Error) : result;

        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action(result.Error);
            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsSuccess)
            {
                return result;
            }
            action();
            return result;
        }

        public static Result<T> OnBoth<T>(this Result result, Func<Result, Result<T>> func) => func(result);

        public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

        public static Result<T2> OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, Result<T2>> func) => func(result);

        public static T2 OnBoth<T1, T2>(this Result<T1> result, Func<Result<T1>, T2> func) => func(result);
    }
}