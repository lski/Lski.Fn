using System;

namespace Lski.Fn
{
    internal struct EitherLeft<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private TLeft _value;

        public EitherLeft(TLeft value)
        {
            _value = value;
        }

        public IEither<TLeft, TRight> Do(Action<TLeft> left, Action<TRight> right) {

            if (left == null) {
                throw new ArgumentNullException(nameof(left));
            }

            left(_value);
            return this;
        }
           
        public T Do<T>(Func<TLeft, T> left, Func<TRight, T> right) => left != null ? left(_value) : throw new ArgumentNullException(nameof(left));
    }
}