using System;

namespace Lski.Fn
{
    public abstract class Result<T> : Result where T : class, struct
    {
        public virtual T Value => throw new InvalidOperationException("Failure should not have a value");

        public Result(bool success) : base(success)
        {
        }
    }
}