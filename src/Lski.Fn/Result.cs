using System;
using System.Diagnostics;

namespace Lski.Fn
{
    /// <summary>
    /// A Result represents an Immutable successful or unsuccessful result of an "action" without the need for exceptions. If successful it can have a Value, otherwise contains an error.
    /// </summary>
    public abstract class Result
    {
        internal Result(bool success)
        {
            IsSuccess = success;
        }

        /// <summary>
        /// States if the result is unsuccessful
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// States if the result was successful
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Returns an error class, with a an error message
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If a result is successful then accessing the Error property throw an exception
        /// </exception>
        public virtual Error Error
        {
            get
            {
                throw new InvalidOperationException("A successful result should not have an error");
            }
        }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        [DebuggerStepThrough]
        public static Result Ok() => new ResultSuccess();

        /// <summary>
        /// Creates a successful result with a value
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// A successful result should not contain a null value. Use Maybe&lt;T&gt; to store potentially empty values.
        /// </exception>
        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T data) => new ResultSuccess<T>(data);

        /// <summary>
        /// Creates an unsuccessful result with error message
        /// </summary>
        [DebuggerStepThrough]
        public static Result Fail(string error) => new ResultFailed(error);

        /// <summary>
        /// Creates an unsuccessful result with passed error of the stated type.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error) => new ResultFailed<T>(error);

        /// <summary>
        /// Creates an unsuccessful result with error message
        /// </summary>
        [DebuggerStepThrough]
        public static Result Fail(Error error) => new ResultFailed(error);

        /// <summary>
        /// Creates an unsuccessful result with passed error of the stated type.
        /// </summary>
        [DebuggerStepThrough]
        public static Result<T> Fail<T>(Error error) => new ResultFailed<T>(error);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public static bool operator ==(Result first, Result second) => first.Equals(second);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public static bool operator !=(Result first, Result second) => !(first == second);

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
            return obj is Result ? Equals((Result)obj) : false;
        }

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        [DebuggerStepThrough]
        public bool Equals(Result other)
        {
            if (this.IsSuccess == other.IsSuccess)
            {
                return true;
            }

            if (this.IsFailure && this.Error.Equals(other.Error))
            {
                return true;
            }

            return false;
        }
    }
}