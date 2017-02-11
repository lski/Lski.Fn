using System;

namespace Lski.Fn
{
    internal struct EitherRight<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private TRight _value;

        public EitherRight(TRight value)
        {
            _value = value;
        }

        public IEither<TLeft, TRight> Do(Action<TLeft> left, Action<TRight> right)
        {

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            right(_value);
            return this;
        }

        public T Do<T>(Func<TLeft, T> left, Func<TRight, T> right)
            => right != null ? right(_value) : throw new ArgumentNullException(nameof(right));
    }
}