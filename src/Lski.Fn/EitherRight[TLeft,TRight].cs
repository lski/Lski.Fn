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

        public bool IsLeft => false;

        public bool IsRight => true;

        public IEither<TLeft, TRight> Do(Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            if (rightAct == null)
            {
                throw new ArgumentNullException(nameof(rightAct));
            }

            rightAct(_value);
            return this;
        }

        public T Do<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc) => rightFunc != null ? rightFunc(_value) : throw new ArgumentNullException(nameof(rightFunc));

        public TRight Right() => _value;

        public T Right<T>(Func<TRight, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public IEither<TLeft, TRight> Right(Action<TRight> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public TLeft Left() => throw new InvalidOperationException("A right-side either does not contain a left value");

        public T Left<T>(Func<TLeft, T> func) => default(T);

        public IEither<TLeft, TRight> Left(Action<TLeft> action) => this;   
    }
}