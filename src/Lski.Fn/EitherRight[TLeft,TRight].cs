using System;

namespace Lski.Fn
{
    internal class EitherRight<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TRight _value;

        public EitherRight(TRight value)
        {
            _value = value;
        }

        public override bool IsLeft => false;

        public override bool IsRight => true;

        public override Either<TLeft, TRight> Do(Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            if (rightAct == null)
            {
                throw new ArgumentNullException(nameof(rightAct));
            }

            rightAct(_value);
            return this;
        }

        public override T Do<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc) => rightFunc != null ? rightFunc(_value) : throw new ArgumentNullException(nameof(rightFunc));

        public override TRight Right() => _value;

        public override T Right<T>(Func<TRight, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public override Either<TLeft, TRight> Right(Action<TRight> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public override TLeft Left() => throw new InvalidOperationException("A right-side either does not contain a left value");

        public override T Left<T>(Func<TLeft, T> func) => default(T);

        public override Either<TLeft, TRight> Left(Action<TLeft> action) => this;   
    }
}