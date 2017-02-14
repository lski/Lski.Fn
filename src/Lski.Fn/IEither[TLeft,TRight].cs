using System;

namespace Lski.Fn
{
    /// <summary>
    /// Represents an Immutable wrapper that can contain either one value or another. So can be described as being left or right sided.
    /// </summary>
    public interface IEither<TLeft, TRight>
    {
        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        T Do<T>(Func<TLeft, T> left, Func<TRight, T> right);

        /// <summary>
        /// Runs only the appropriate action and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Do(Action<TLeft> left, Action<TRight> right);

        /// <summary>
        /// Runs only if this object is right sided, otherwise returns default(T)
        /// </summary>
        T Right<T>(Func<TRight, T> func);

        /// <summary>
        /// Runs only the appropriate function and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Right(Action<TRight> action);

        /// <summary>
        /// Runs only if this object is left sided, otherwise returns default(T)
        /// </summary>
        T Left<T>(Func<TLeft, T> func);

        /// <summary>
        /// Runs only if this object is left sided, and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Left(Action<TLeft> action);

        /// <summary>
        /// States if this either is left sided or not
        /// </summary>
        bool IsLeft { get; }

        /// <summary>
        /// States if this either is right sided or not
        /// </summary>
        bool IsRight { get; }
    }
}