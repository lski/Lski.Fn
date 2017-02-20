using System;
using System.Diagnostics;

namespace Lski.Fn
{
    internal class ResultFailed<T> : Result<T>
    {
        public override Error Error { get; }

        public ResultFailed(string message) : base(false)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "An unsuccesful result requires an error message");
            }

            Error = new Error(message);
        }

        public ResultFailed(Error error) : base(false)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error), "An unsuccesful result requires an error");
            }

            Error = error;
        }

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override string ToString() => Error.ToString();

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override int GetHashCode() => Error.GetHashCode();
    }
}