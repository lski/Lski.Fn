using System;
using System.Diagnostics;

namespace Lski.Fn
{
    internal class ResultSuccess<T> : Result<T>
    {
        private T _data;

        public override T Value => _data;

        public ResultSuccess(T data) : base(true)
        {
            if (data == null)
            {
                throw new ArgumentNullException("A success should not be null");
            }

            _data = data;
        }

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override string ToString() => _data.ToString();

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override int GetHashCode() => _data.GetHashCode();
    }
}