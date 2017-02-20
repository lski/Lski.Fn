using System;

namespace Lski.Fn
{
    /// <summary>
    /// Contains funcitons for creating left or right sided Either objects
    /// </summary>
    public abstract class Either<TLeft, TRight>
    {
        /// <summary>
        /// States if this either is left sided or not
        /// </summary>
        public abstract bool IsLeft { get; }

        /// <summary>
        /// States if this either is right sided or not
        /// </summary>
        public abstract bool IsRight { get; }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        public abstract T Do<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc);

        /// <summary>
        /// Runs only the appropriate action and returns this either object unchanged.
        /// </summary>
        public abstract Either<TLeft, TRight> Do(Action<TLeft> leftAct, Action<TRight> rightAct);

        /// <summary>
        /// Returns the value for right, if this is left sided it throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public abstract TRight Right();

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public abstract T Right<T>(Func<TRight, T> func);

        /// <summary>
        /// Runs only the appropriate function and returns this either object unchanged.
        /// </summary>
        public abstract Either<TLeft, TRight> Right(Action<TRight> action);

        /// <summary>
        /// Returns the value for left, if this is right sided it throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public abstract TLeft Left();

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public abstract T Left<T>(Func<TLeft, T> func);

        /// <summary>
        /// Runs only if this object is left sided, and returns this either object unchanged.
        /// </summary>
        public abstract Either<TLeft, TRight> Left(Action<TLeft> action);

        /// <summary>
        /// Converts a value of the left side type to a left-sided either
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TLeft left) => Either.Left<TLeft, TRight>(left);

        /// <summary>
        /// Converts a value of the left side type to a left-sided either
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TRight right) => Either.Right<TLeft, TRight>(right);
    }


}