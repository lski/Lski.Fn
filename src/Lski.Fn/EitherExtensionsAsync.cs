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
        /// Returns the value if this is a left sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        public static async Task<TLeft> Left<TLeft, TRight>(this Task<Either<TLeft, TRight>> task)
        {
            var either = await task.ConfigureAwait(false);
            return either.Left();
        }

        /// <summary>
        /// Returns the value if this is a right sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either
        /// </exception>
        public static async Task<TLeft> Right<TLeft, TRight>(this Task<Either<TLeft, TRight>> task)
        {
            var either = await task.ConfigureAwait(false);
            return either.Left();
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> LeftOrRight<T, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            return either.IsLeft ?
                await leftFunc(either.Left()).ConfigureAwait(false)
                : await rightFunc(either.Right()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is right sided
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, Task<TRightOut>> func)
        {
            if (either.IsRight)
            {
                var value = await func(either.Right()).ConfigureAwait(false);
                Either.Right<TLeft, TRightOut>(value);
            }

            return Either.Left<TLeft, TRightOut>(either.Left());
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<TLeftOut>> func)
        {
            if (either.IsLeft)
            {
                var value = await func(either.Left()).ConfigureAwait(false);
                return Either.Left<TLeftOut, TRight>(value);
            }

            return Either.Right<TLeftOut, TRight>(either.Right());
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> LeftOrRight<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return await either.LeftOrRight(leftFunc, rightFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, Task<TRightOut>> func)
        {
            var either = await task.ConfigureAwait(false);

            if (either.IsRight)
            {
                var value = await func(either.Right()).ConfigureAwait(false);
                Either.Right<TLeft, TRightOut>(value);
            }

            return Either.Left<TLeft, TRightOut>(either.Left());
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<TLeftOut>> func)
        {
            var either = await task.ConfigureAwait(false);

            if (either.IsLeft)
            {
                var value = await func(either.Left()).ConfigureAwait(false);
                return Either.Left<TLeftOut, TRight>(value);
            }

            return Either.Right<TLeftOut, TRight>(either.Right());
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<T> LeftOrRight<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return either.LeftOrRight(leftFunc, rightFunc);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a left sided either
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, TRightOut> func)
        {
            var either = await task.ConfigureAwait(false);
            return either.Right(func);
        }

        /// <summary>
        /// Runs only if this object is left sided
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, TLeftOut> func)
        {
            var either = await task.ConfigureAwait(false);
            return either.Left(func);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> LeftOrRight<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            var either = await task.ConfigureAwait(false);
            either.LeftOrRight(leftAct, rightAct);
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