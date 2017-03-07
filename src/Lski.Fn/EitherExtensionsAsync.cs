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
        /// Returns the value if this is a left-sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a right sided either
        /// </exception>
        public static async Task<TLeft> ToLeft<TLeft, TRight>(this Task<Either<TLeft, TRight>> task)
        {
            var either = await task.ConfigureAwait(false);
            return either.ToLeft();
        }

        /// <summary>
        /// Returns the value if this is a right sided either, otherwise it throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is a Left sided either
        /// </exception>
        public static async Task<TRight> ToRight<TLeft, TRight>(this Task<Either<TLeft, TRight>> task)
        {
            var either = await task.ConfigureAwait(false);
            return either.ToRight();
        }

        /// <summary>
        /// Returns the value if this is a left sided either, otherwise returns the default value
        /// </summary>
        public static async Task<TLeft> ToLeft<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, TLeft defaultValue)
        {
            var either = await task.ConfigureAwait(false);
            return either.ToLeft(defaultValue);
        }

        /// <summary>
        /// Returns the value if this is a right sided either, otherwise returns the default value
        /// </summary>
        public static async Task<TRight> ToRight<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, TRight defaultValue)
        {
            var either = await task.ConfigureAwait(false);
            return either.ToRight(defaultValue);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> ToValue<T, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            return either.IsLeft ?
                await leftFunc(either.ToLeft()).ConfigureAwait(false)
                : await rightFunc(either.ToRight()).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the appropriate func depending on the side of the either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRightOut>> LeftOrRight<TLeftOut, TRightOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<TLeftOut>> leftFunc, Func<TRight, Task<TRightOut>> rightFunc)
        {
            if (either.IsLeft)
            {
                return await leftFunc(either.ToLeft()).ConfigureAwait(false);
            }

            return await rightFunc(either.ToRight()).ConfigureAwait(false);
        }

        /// <summary>
        /// Only runs func if this either is left sided, a new Either is return
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task<TLeftOut>> func)
        {
            if (either.IsLeft)
            {
                return await func(either.ToLeft()).ConfigureAwait(false);
            }

            return either.ToRight();
        }

        /// <summary>
        /// Only runs function if this either is right sided, a new Either is return
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, Task<TRightOut>> func)
        {
            if (either.IsRight)
            {
                return await func(either.ToRight()).ConfigureAwait(false);
            }

            return either.ToLeft();
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> ToValue<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<T>> leftFunc, Func<TRight, Task<T>> rightFunc)
        {
            var either = await task.ConfigureAwait(false);
            return await either.ToValue(leftFunc, rightFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the appropriate func depending on the side of the either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRightOut>> LeftOrRight<TLeftOut, TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<TLeftOut>> leftFunc, Func<TRight, Task<TRightOut>> rightFunc)
        {
            var either = await task.ConfigureAwait(false);

            if (either.IsLeft)
            {
                return await leftFunc(either.ToLeft()).ConfigureAwait(false);
            }

            return await rightFunc(either.ToRight()).ConfigureAwait(false);
        }

        /// <summary>
        /// Only runs func if this either is left sided, a new Either is return
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, Task<TLeftOut>> func)
        {
            var either = await task.ConfigureAwait(false);

            if (either.IsLeft)
            {
                return await func(either.ToLeft()).ConfigureAwait(false);
            }

            return either.ToRight();
        }

        /// <summary>
        /// Only runs func if this either is right sided, a new Either is return
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, Task<TRightOut>> func)
        {
            var either = await task.ConfigureAwait(false);

            if (either.IsRight)
            {
                return await func(either.ToRight()).ConfigureAwait(false);
            }

            return either.ToLeft();
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<T> ToValue<T, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc)
        {
            var either = await task.ConfigureAwait(false);

            return either.ToValue(leftFunc, rightFunc);
        }

        /// <summary>
        /// Runs the appropriate func depending on the side of the either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightFunc is null, or if left-sided and leftFunc is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRightOut>> LeftOrRight<TLeftOut, TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, TLeftOut> leftFunc, Func<TRight, TRightOut> rightFunc)
        {
            var either = await task.ConfigureAwait(false);

            return either.LeftOrRight(leftFunc, rightFunc);
        }

        /// <summary>
        /// Only runs func if this either is left sided, a new Either is returned
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeftOut, TRight>> Left<TLeftOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TLeft, TLeftOut> func)
        {
            var either = await task.ConfigureAwait(false);

            return either.Left(func);
        }

        /// <summary>
        /// Only runs func if this either is right sided, a new Either is returned
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the func is null or returned value from the func is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRightOut>> Right<TRightOut, TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Func<TRight, TRightOut> func)
        {
            var either = await task.ConfigureAwait(false);

            return either.Right(func);
        }

        /// <summary>
        /// Runs only the appropriate function and returns the value from than function.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Only thrown if right-sided and rightAct is null, or if left-sided and leftAct is null. Otherwise not thrown
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> LeftOrRight<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            var either = await task.ConfigureAwait(false);

            return either.LeftOrRight(leftAct, rightAct);
        }

        /// <summary>
        /// Runs only if this object is right sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the action is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> Right<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TRight> action)
        {
            var either = await task.ConfigureAwait(false);

            return either.Right(action);
        }

        /// <summary>
        /// Runs only if this object is left sided, otherwise throws an exception as a return value is expected, use in combination with IsLeft/IsRight
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If the action is null
        /// </exception>
        [DebuggerStepThrough]
        public static async Task<Either<TLeft, TRight>> Left<TLeft, TRight>(this Task<Either<TLeft, TRight>> task, Action<TLeft> action)
        {
            var either = await task.ConfigureAwait(false);

            return either.Left(action);
        }
    }
}