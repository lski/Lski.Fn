using System;

namespace Lski.Fn
{
    /// <summary>
    /// Contains extension methods for Maybe&lt;T&gt; objects
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Returns either the stored value or the default if contains nothing
        /// </summary>
        public static T Unwrap<T>(this Maybe<T> maybe, T defaultValue = default(T))
        {
            return maybe.HasValue ? maybe.Value : defaultValue;
        }

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, Error error)
        {
            return maybe.HasValue ? maybe.Value.ToSuccess() : Result.Fail<T>(error);
        }

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string error) => maybe.ToResult(error);

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static Maybe<T> Do<T>(this Maybe<T> maybe, Action<T> action) => maybe.Bind(action);

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static Maybe<TOut> Do<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> func) => maybe.Bind(func);

        /// <summary>
        /// Performs the action if Maybe contains a value and returns the same Maybe for chaining.
        /// </summary>
        public static Maybe<T> Bind<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }

            return maybe;
        }

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None&lt;TOut&gt;
        /// </summary>
        public static Maybe<TOut> Bind<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> func)
        {
            return maybe.HasValue ? func(maybe.Value) : Maybe.None<TOut>();
        }
    }
}