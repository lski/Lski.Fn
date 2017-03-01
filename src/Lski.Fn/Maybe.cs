namespace Lski.Fn
{
    /// <summary>
    /// Contains funcitons for creating Maybe&lt;T&gt; objects
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Creates an empty Maybe of the specified type that represents "nothing". Equivalent to Maybe.Create&lt;T&gt;(null).
        /// </summary>
        public static Maybe<T> None<T>() => new Maybe<T>();

        /// <summary>
        /// Create a new Maybe object of the type passed
        /// </summary>
        public static Maybe<T> Create<T>(T value)
        {
            return value == null ? Maybe.None<T>() : new Maybe<T>(value);
        }

        /// <summary>
        /// Convert a value to a Maybe object of the same type
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this T value) => Create<T>(value);
    }
}