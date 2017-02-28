using System;
using System.Threading.Tasks;

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
        public static Maybe<T> Create<T>(T value)
        {
            return value == null ? Maybe<T>.None() : new Maybe<T>(value);
        }

        /// <summary>
        /// Convert a value to a Maybe object of the same type
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this T value) => Create<T>(value);

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
            return maybe.HasValue ? maybe.Value.ToResult() : Result.Fail<T>(error);
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
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None
        /// </summary>
        public static Maybe<TOut> Bind<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> func)
        {
            return maybe.HasValue ? func(maybe.Value) : Maybe<TOut>.None();
        }

        /// <summary>
        /// Returns either the stored value or the default if contains nothing
        /// </summary>
        public static async Task<T> Unwrap<T>(this Task<Maybe<T>> task, T defaultValue = default(T))
        {
            var maybe = await task.ConfigureAwait(false);
            return maybe.Unwrap(defaultValue);
        }

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static async Task<Result<T>> ToResult<T>(this Task<Maybe<T>> task, Error error)
        {
            var maybe = await task.ConfigureAwait(false);
            return maybe.ToResult(error);
        }

        /// <summary>
        /// Convert the maybe into a result, a value means success, nothing means failure
        /// </summary>
        public static async Task<Result<T>> ToResult<T>(this Task<Maybe<T>> task, string error)
        {
            var maybe = await task.ConfigureAwait(false);
            return maybe.ToResult(error);
        }

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static async Task<Maybe<T>> Do<T>(this Task<Maybe<T>> maybe, Action<T> action) => await maybe.Bind(action);

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static async Task<Maybe<TOut>> Do<T, TOut>(this Task<Maybe<T>> maybe, Func<T, Maybe<TOut>> func) => await maybe.Bind(func);

        /// <summary>
        /// Performs the action if Maybe contains a value and returns the same Maybe for chaining.
        /// </summary>
        public static async Task<Maybe<T>> Bind<T>(this Task<Maybe<T>> task, Action<T> action)
        {
            var maybe = await task.ConfigureAwait(false);
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }

            return maybe;
        }

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None
        /// </summary>
        public static async Task<Maybe<TOut>> Bind<T, TOut>(this Task<Maybe<T>> task, Func<T, Maybe<TOut>> func)
        {
            var maybe = await task.ConfigureAwait(false);
            return maybe.HasValue ? func(maybe.Value) : Maybe<TOut>.None();
        }

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static async Task<Maybe<TOut>> Do<T, TOut>(this Maybe<T> maybe, Func<T, Task<Maybe<TOut>>> func) => await maybe.Bind(func);

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None
        /// </summary>
        public static async Task<Maybe<TOut>> Bind<T, TOut>(this Maybe<T> maybe, Func<T, Task<Maybe<TOut>>> func)
        {
            if (maybe.HasValue)
            {
                return await func(maybe.Value);
            }

            return Maybe<TOut>.None();
        }

        /// <summary>
        /// Alias for Bind
        /// </summary>
        public static async Task<Maybe<TOut>> Do<T, TOut>(this Task<Maybe<T>> maybe, Func<T, Task<Maybe<TOut>>> func) => await maybe.Bind(func);

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None
        /// </summary>
        public static async Task<Maybe<TOut>> Bind<T, TOut>(this Task<Maybe<T>> task, Func<T, Task<Maybe<TOut>>> func)
        {
            var maybe = await task.ConfigureAwait(false);

            if (maybe.HasValue)
            {
                return await func(maybe.Value);
            }

            return Maybe<TOut>.None();
        }
    }
}