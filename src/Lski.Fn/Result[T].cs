using System;

namespace Lski.Fn
{
    /// <summary>
    /// Extends a result so it can contain a value or an error, but be of the necessary type.
    /// </summary>
    public abstract class Result<T> : Result
    {
        /// <summary>
        /// Returns the value store for this result
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// An unsuccessful result should never contain a value.
        /// </exception>
        public virtual T Value => throw new InvalidOperationException("An unsuccessful result should not have a value");

        internal Result(bool success) : base(success)
        {
        }
    }
}