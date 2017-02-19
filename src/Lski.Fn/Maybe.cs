using System;

namespace Lski.Fn
{
    /// <summary>
    /// A Maybe object is a wrapper around a potentially null value. So either has a value or contains "nothing" of its particular type
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Creates an empty Maybe of the specified type that represents "nothing"
        /// </summary>
        public static Maybe<T> None<T>() => new Maybe<T>();

        /// <summary>
        /// Create a new Maybe object of the type passed
        /// </summary>
        public static Maybe<T> Create<T>(T value) => value == null ? Maybe<T>.None() : new Maybe<T>(value);

        /// <summary>
        /// Convert a value to a Maybe object of the same type
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this T value) => Create<T>(value);

        /// <summary>
        /// Returns either the stored value or the default if contains nothing
        /// </summary>
        public static T Unwrap<T>(this Maybe<T> maybe, T defaultValue = default(T)) => maybe.HasValue ? maybe.Value : defaultValue;

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, Error error) => maybe.HasValue ? maybe.Value.ToResult() : Result.Fail<T>(error);

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string error) => maybe.ToResult(new Error(error));

        /// <summary>
        /// Perform the action if Maybe contains a value
        /// </summary>
        public static Maybe<T> Do<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }

            return maybe;
        }

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static Maybe<TOut> Do<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> func) => maybe.Bind(func);

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a "none" Maybe
        /// </summary>
        public static Maybe<TOut> Bind<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> func) => maybe.HasValue ? func(maybe.Value) : Maybe<TOut>.None();
    }
}