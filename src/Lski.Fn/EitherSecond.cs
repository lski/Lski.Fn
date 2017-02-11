using System;

namespace Lski.Fn
{
    internal struct EitherSecond<TFirst, TSecond> : IEither<TFirst, TSecond>
    {
        private TSecond _value;

        public EitherSecond(TSecond value)
        {
            _value = value;
        }

        public IEither<TFirst, TSecond> Do(Action<TFirst> first, Action<TSecond> second)
        {
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            second(_value);
            return this;
        }

        public T Do<T>(Func<TFirst, T> first, Func<TSecond, T> second) => second != null ? second(_value) : throw new ArgumentNullException(nameof(second));
    }
}