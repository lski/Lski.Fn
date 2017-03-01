using System;
using System.Threading.Tasks;

namespace Lski.Fn
{
    /// <summary>
    /// Contains async extension pass-thru functions for Maybe&lt;T&gt; objects
    /// </summary>
    public static class MaybeAsyncExtensions
    {
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
            return maybe.HasValue ? func(maybe.Value) : Maybe.None<TOut>();
        }

        /// <summary>
        /// If contains a value performs the function and returns a new Maybe, otherwise returns a new Maybe.None
        /// </summary>
        public static async Task<Maybe<TOut>> Bind<T, TOut>(this Maybe<T> maybe, Func<T, Task<Maybe<TOut>>> func)
        {
            if (maybe.HasValue)
            {
                return await func(maybe.Value);
            }

            return Maybe.None<TOut>();
        }
        
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

            return Maybe.None<TOut>();
        }
    }
}