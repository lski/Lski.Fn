using System;

namespace Lski.Fn
{
    /// <summary>
    /// Represents an Immutable wrapper that can contain either one value or another. So can be
    /// described as being left or right sided.
    /// </summary>
    public interface IEither<out TLeft, out TRight>
    {
        /// <summary>
        /// States if this either is left sided or not
        /// </summary>
        bool IsLeft { get; }

        /// <summary>
        /// States if this either is right sided or not
        /// </summary>
        bool IsRight { get; }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        T Do<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc);

        /// <summary>
        /// Runs only the appropriate action and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Do(Action<TLeft> leftAct, Action<TRight> rightAct);

        /// <summary>
        /// Returns the value for right, if this is left sided it throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        TRight Right();

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        T Right<T>(Func<TRight, T> func);

        /// <summary>
        /// Runs only the appropriate function and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Right(Action<TRight> action);

        /// <summary>
        /// Returns the value for left, if this is right sided it throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        TLeft Left();

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        T Left<T>(Func<TLeft, T> func);

        /// <summary>
        /// Runs only if this object is left sided, and returns this either object unchanged.
        /// </summary>
        IEither<TLeft, TRight> Left(Action<TLeft> action);
    }
}