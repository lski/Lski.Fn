using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lski.Fn
{
    /// <summary>
    /// Contains async extension pass-thru functions for Either&lt;TLeft, TRight&gt; objects
    /// </summary>
    public static class EitherExtensionsAsync
    {
        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> Do<T, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            return either.IsLeft ?
                await either.Left(leftFunc).ConfigureAwait(false)
                : await either.Right(rightFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Right<T, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, Task<T>> func)
        {
            return await func(either.Right()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Left<T, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<T>> func)
        {
            return await func(either.Left()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> Do<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return await either.Do(leftFunc, rightFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Right<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, Task<T>> func)
        {
            var either = await task.ConfigureAwait(false);
            return await func(either.Right()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Left<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<T>> func)
        {
            var either = await task.ConfigureAwait(false);
            return await func(either.Left()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> Do<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return either.Do(leftFunc, rightFunc);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Right<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, T> func)
        {
            var either = await task.ConfigureAwait(false);
            return func(either.Right());
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> Left<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, T> func)
        {
            var either = await task.ConfigureAwait(false);
            return func(either.Left());
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> Do<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            var either = await task.ConfigureAwait(false);
            either.Do(leftAct, rightAct);
            return either;
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> Right<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TRight> action)
        {
            var either = await task.ConfigureAwait(false);
            action(either.Right());
            return either;
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> Left<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TLeft> action)
        {
            var either = await task.ConfigureAwait(false);
            action(either.Left());
            return either;
        }
    }
}