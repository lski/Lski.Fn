using System;

namespace Lski.Fn
{
    /// <summary>
    /// A base class for Either
    /// </summary>
    /// <remarks>
    /// An Either is an Immutable object that represents a choice of two possible options. Either we are working with a value on the "left" or a value on the "right".
    ///
    /// An either can not be both left and right and must have a value. This gives a type safety to anything excepting the either object of the types it needs to deal with.
    /// It has the benefit of giving an users of an API that a complete over-view of the potential values coming back, meaning no understanding of the function is needed to deal
    /// with casting to particular types on certain responses etc.
    /// </remarks>
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
        /// Returns the value if this is a left sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either
        /// </exception>
        public abstract TLeft Left();

        /// <summary>
        /// Runs only if this either is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        public abstract T Left<T>(Func<TLeft, T> func);

        /// <summary>
        /// Runs only if either is left sided, and returns the either unchanged for chaining. Does NOT throw exception if a right-sided either.
        /// </summary>
        public abstract Either<TLeft, TRight> Left(Action<TLeft> action);

        /// <summary>
        /// Returns the value if this is a right sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either
        /// </exception>
        public abstract TRight Right();

        /// <summary>
        /// Runs only if this either is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either
        /// </exception>
        public abstract T Right<T>(Func<TRight, T> func);

        /// <summary>
        /// Runs only if either is right sided, and returns the either unchanged for chaining. Does NOT throw exception if a right-sided either.
        /// </summary>
        public abstract Either<TLeft, TRight> Right(Action<TRight> action);

        /// <summary>
        /// Implicitly converts a value of the left side type to a left-sided either
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TLeft left) => Either.Left<TLeft, TRight>(left);

        /// <summary>
        /// Implicitly converts a value of the left side type to a left-sided either
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TRight right) => Either.Right<TLeft, TRight>(right);

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        public abstract override string ToString(); 

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        public override int GetHashCode()
        {
            return this.IsLeft
                ? this.Left().GetHashCode()
                : this.Right().GetHashCode();
        }

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator ==(Either<TLeft, TRight> first, Either<TLeft, TRight> second) => first.Equals(second);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator !=(Either<TLeft, TRight> first, Either<TLeft, TRight> second) => !(first == second);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public override bool Equals(object obj)
        {
            Either<TLeft, TRight> other;

            if (obj is Either<TLeft, TRight>)
            {
                other = (Either<TLeft, TRight>)obj;
            }
            else if (obj is TLeft)
            {
                other = (TLeft)obj;
            }
            else if (obj is TRight)
            {
                other = (TRight)obj;
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
        public bool Equals(Either<TLeft, TRight> other)
        {
            if (this.IsLeft != other.IsLeft)
            {
                return false;
            }

            return this.Do(l =>
            {
                var otherLeft = other.Left();
                return (l != null && l.Equals(otherLeft)) || (l == null && otherLeft == null);
            },
            r =>
            {
                var otherRight = other.Right();
                return (r != null && r.Equals(otherRight)) || (r == null && otherRight == null);
            });
        }
    }
}