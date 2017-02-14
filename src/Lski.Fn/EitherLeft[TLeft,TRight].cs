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

        public IEither<TLeft, TRight> Do(Action<TLeft> left, Action<TRight> right)
        {

            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            left(_value);
            return this;
        }

        public T Do<T>(Func<TLeft, T> left, Func<TRight, T> right) => left != null ? left(_value) : throw new ArgumentNullException(nameof(left));

        public T Right<T>(Func<TRight, T> func) => default(T);

        public T Left<T>(Func<TLeft, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public IEither<TLeft, TRight> Right(Action<TRight> action) => this;

        public IEither<TLeft, TRight> Left(Action<TLeft> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public bool IsLeft => true;

        public bool IsRight => false;
    }
}