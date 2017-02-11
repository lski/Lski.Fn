using System;

namespace Lski.Fn
{
    public static class Maybe
    {
        public static Maybe<T> Create<T>(T value) => new Maybe<T>(value);

        public static Maybe<T> ToMaybe<T>(this T value) => new Maybe<T>(value);
    }

    public struct Maybe<T>
    {
        private T _value;

        internal Maybe(T value)
        {
            _value = value;
        }

        public bool HasValue => _value != null;

        public bool HasNoValue => !HasValue;

        public T Value => HasValue ? _value : throw new InvalidOperationException("Value is null");

        public static bool operator ==(Maybe<T> maybe, T value) => maybe.HasNoValue ? false : maybe.Value.Equals(value);

        public static bool operator !=(Maybe<T> maybe, T value) => !(maybe == value);

        public static bool operator ==(Maybe<T> first, Maybe<T> second) => first.Equals(second);

        public static bool operator !=(Maybe<T> first, Maybe<T> second) => !(first == second);

        public override string ToString() => HasNoValue ? "No value" : _value.ToString();

        public override int GetHashCode() => HasNoValue ? 0 : _value.GetHashCode();

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