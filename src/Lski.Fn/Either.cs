using System;
using System.Diagnostics;

namespace Lski.Fn
{
    /// <summary>
    /// Contains funcitons for creating left or right sided Either objects
    /// </summary>
    public static class Either
    {
        /// <summary>
        /// Creates a left sided either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If value is null
        /// </exception>
        [DebuggerStepThrough]
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Creates a right sided either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If value is null
        /// </exception>
        [DebuggerStepThrough]
        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value) => new EitherRight<TLeft, TRight>(value);

        /// <summary>
        /// Converts a varible to a left sided either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If value is null
        /// </exception>
        [DebuggerStepThrough]
        public static Either<TLeft, TRight> ToLeft<TLeft, TRight>(this TLeft value) => new EitherLeft<TLeft, TRight>(value);

        /// <summary>
        /// Converts a varible to a right sided either
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If value is null
        /// </exception>
        [DebuggerStepThrough]
        public static Either<TLeft, TRight> ToRight<TLeft, TRight>(this TRight value) => new EitherRight<TLeft, TRight>(value);
    }
}