using System;

namespace Lski.Fn
{
    /// <summary>
    /// A Maybe object is a wrapper around a potentially null value. So either has a value or
    /// contains "nothing" of its particular type
    /// </summary>
    public class Maybe<T>
    {
        private readonly T _value;

        internal Maybe()
        {
        }

        internal Maybe(T value)
        {
            _value = value != null ? value : throw new ArgumentNullException(nameof(value), "The value for Maybe can not be null");
        }

        /// <summary>
        /// True if the Maybe contains a value
        /// </summary>
        public bool HasValue => _value != null;

        /// <summary>
        /// True if the Maybe does NOT contain a value, therefore represents "nothing"
        /// </summary>
        public bool HasNoValue => !HasValue;

        /// <summary>
        /// Returns the Value of this if this Maybe contains a value.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If the Maybe represents "nothing" (value == None)
        /// </exception>
        public T Value => HasValue ? _value : throw new InvalidOperationException("Maybe has no value");

        /// <summary>
        /// Creates an empty Maybe of the specified type that represents "nothing"
        /// </summary>
        public static Maybe<T> None() => new Maybe<T>();

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator ==(Maybe<T> maybe, T value) => maybe.HasValue && maybe.Value.Equals(value);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator !=(Maybe<T> maybe, T value) => !(maybe == value);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator ==(Maybe<T> first, Maybe<T> second) => first.Equals(second);

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public static bool operator !=(Maybe<T> first, Maybe<T> second) => !(first == second);

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        public override string ToString() => HasNoValue ? "None" : _value.ToString();

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        public override int GetHashCode() => HasNoValue ? 0 : _value.GetHashCode();

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                obj = new Maybe<T>((T)obj);
            }

            if (!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        /// <summary>
        /// Compare the underlying values
        /// </summary>
        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return _value.Equals(other._value);
        }
    }
}