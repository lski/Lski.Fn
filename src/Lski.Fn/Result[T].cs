using System;
using System.Diagnostics;

namespace Lski.Fn
{
    /// <summary>
    /// Extends a result so it can contain either a value of a particular type or an error, but not both.
    ///
    /// So if successful it will have a Value, otherwise contains an error.
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

        /// <summary>
        /// Allows implicit casting between result and its underlying type
        /// </summary>
        [DebuggerStepThrough]
        public static implicit operator Result<T>(T value) => Result.Success(value);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public static bool operator ==(Result<T> first, Result<T> second) => first.Equals(second);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public static bool operator !=(Result<T> first, Result<T> second) => !(first == second);

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override abstract string ToString();

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override abstract int GetHashCode();

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            Result other;

            if (obj is T)
            {
                other = Result.Success((T)obj);
            }
            else if (obj is Result<T>)
            {
                other = (Result<T>)obj;
            }
            else
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public bool Equals(Result<T> other)
        {
            if (this.IsSuccess)
            {
                if (other.IsFailure || !this.Value.Equals(other.Value))
                {
                    return false;
                }

                return true;
            }

            return this.Error.Equals(other.Error);
        }
    }
}