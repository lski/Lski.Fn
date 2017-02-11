using System;
using System.Diagnostics;

namespace Lski.Fn
{
    public abstract class Result
    {
        private bool _success;

        protected Result(bool success)
        {
            _success = success;
        }

        public bool IsFailure => !_success;

        public bool IsSuccess => _success;

        public virtual string Error => throw new InvalidOperationException("Success doesnt have an error");

        [DebuggerStepThrough]
        public static Result Ok() => new ResultSuccess();

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T data) => new ResultSuccess<T>(data);

        [DebuggerStepThrough]
        public static Result Fail(string error) => new ResultFailed(error);

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error) => new ResultFailed<T>(error);
    }
}