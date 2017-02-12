using System;

namespace Lski.Fn
{
    internal struct EitherFirst<TFirst, TSecond> : IEither<TFirst, TSecond>
    {
        private TFirst _value;

        public EitherFirst(TFirst value)
        {
            _value = value;
        }

        public IEither<TFirst, TSecond> Do(Action<TFirst> first, Action<TSecond> second) {

            if (first == null) {
                throw new ArgumentNullException(nameof(first));
            }

            first(_value);
            return this;
        }
           
        public T Do<T>(Func<TFirst, T> first, Func<TSecond, T> second) => first != null ? first(_value) : throw new ArgumentNullException(nameof(first));
    }
}