using System;
using System.Threading.Tasks;

namespace Lski.Fn
{
    /// <summary>
    /// Contains funcitons for creating left or right sided IEither objects
    /// </summary>
    public static class Either
    {
        /// <summary>
        /// Creates a left sided either of the specified types
        /// </summary>
        public static IEither<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Creates a right sided either of the specified types
        /// </summary>
        public static IEither<TLeft, TRight> Right<TLeft, TRight>(TRight value) => new EitherRight<TLeft, TRight>(value);

        /// <summary>
        /// Converts a varible to a left sided either of the specified types
        /// </summary>
        public static IEither<TLeft, TRight> ToLeft<TLeft, TRight>(this TLeft value) => new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Converts a varible to a right sided either of the specified types
        /// </summary>
        public static IEither<TLeft, TRight> ToRight<TLeft, TRight>(this TRight value) => new EitherRight<TLeft, TRight>(value);

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        public static async Task<T> Do<T, TLeft, TRight>(this IEither<TLeft, TRight> either, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
            => either.IsLeft ? await either.Left(leftFunc).ConfigureAwait(false) : await either.Right(rightFunc).ConfigureAwait(false);

        /// <summary>
        /// Runs only if this object is right sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Right<T, TLeft, TRight>(this IEither<TLeft, TRight> either, Func<TRight, Task<T>> func) => await func(either.Right()).ConfigureAwait(false);

        /// <summary>
        /// Runs only if this object is left sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Left<T, TLeft, TRight>(this IEither<TLeft, TRight> either, Func<TLeft, Task<T>> func) => await func(either.Left()).ConfigureAwait(false);

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        public static async Task<T> Do<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return await either.Do(leftFunc, rightFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Right<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TRight, Task<T>> func)
        {
            var either = await task.ConfigureAwait(false);
            return await func(either.Right()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Left<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TLeft, Task<T>> func)
        {
            var either = await task.ConfigureAwait(false);
            return await func(either.Left()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        public static async Task<T> Do<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return either.Do(leftFunc, rightFunc);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Right<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TRight, T> func)
        {
            var either = await task.ConfigureAwait(false);
            return func(either.Right());
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<T> Left<T, TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Func<TLeft, T> func)
        {
            var either = await task.ConfigureAwait(false);
            return func(either.Left());
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        public static async Task<IEither<TLeft, TRight>> Do<TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            var either = await task.ConfigureAwait(false);
            either.Do(leftAct, rightAct);
            return either;
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<IEither<TLeft, TRight>> Right<TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Action<TRight> action)
        {
            var either = await task.ConfigureAwait(false);
            action(either.Right());
            return either;
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise returns default(T)
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either, use in combination with IsLeft/IsRight
        /// </exception>
        public static async Task<IEither<TLeft, TRight>> Left<TLeft, TRight>(this Task<IEither<TLeft, TRight>> task, Action<TLeft> action)
        {
            var either = await task.ConfigureAwait(false);
            action(either.Left());
            return either;
        }
    }
}