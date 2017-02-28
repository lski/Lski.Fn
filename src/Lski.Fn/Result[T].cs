﻿using System;
using System.Diagnostics;

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
        public virtual T Value
        {
            get
            {
                throw new InvalidOperationException("An unsuccessful result should not have a value");
            }
        }

        internal Result(bool success) : base(success)
        {
        }

        /// <summary>
        /// Allows implicit casting between result and its underlying type
        /// </summary>
        [DebuggerStepThrough]
        public static implicit operator Result<T>(T value) => Result.Ok(value);

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
                other = Result.Ok((T)obj);
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