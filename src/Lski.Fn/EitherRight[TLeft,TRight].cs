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

        public T Do<T>(Func<TLeft, T> left, Func<TRight, T> right) => right != null ? right(_value) : throw new ArgumentNullException(nameof(right));

        public T Right<T>(Func<TRight, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public T Left<T>(Func<TLeft, T> func) => default(T);

        public IEither<TLeft, TRight> Right(Action<TRight> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public IEither<TLeft, TRight> Left(Action<TLeft> action) => this;
        
        public bool IsLeft => false;

        public bool IsRight => true;
    }
}