using System;
using System.Diagnostics;

namespace Lski.Fn
{
    internal class ResultSuccess<T> : Result<T>
    {
        public override T Value { get; }

        public ResultSuccess(T data) : base(true)
        {
            if (data == null)
            {
                throw new ArgumentNullException("A success should not be null");
            }

            Value = data;
        }

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override int GetHashCode() => Value.GetHashCode();
    }
}