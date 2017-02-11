using System;

namespace Lski.Fn
{
    public abstract class Result<T> : Result
    {
        public virtual T Value => throw new InvalidOperationException("Failure doesnt have a Value");

        public Result(bool success) : base(success)
        {
        }
    }
}