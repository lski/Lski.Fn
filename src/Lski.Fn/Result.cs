using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public abstract class Result
    {
        protected Result(bool success)
        {
            IsSuccess = success;
        }

        public bool IsFailure => !IsSuccess;

        public bool IsSuccess { get; }

        public virtual Error Error => throw new InvalidOperationException("Success should not have an error");

        [DebuggerStepThrough]
        public static Result Ok() => new ResultSuccess();

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T data) => new ResultSuccess<T>(data);

        [DebuggerStepThrough]
        public static Result Fail(string error) => new ResultFailed(error);

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error) => new ResultFailed<T>(error);

        [DebuggerStepThrough]
        public static Result Fail(Error error) => new ResultFailed(error);

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(Error error) => new ResultFailed<T>(error);
    }
}