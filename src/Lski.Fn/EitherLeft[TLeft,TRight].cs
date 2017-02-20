using System;

namespace Lski.Fn
{
    internal class EitherLeft<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TLeft _value;

        public EitherLeft(TLeft value)
        {
            _value = value;
        }

        public override bool IsLeft => true;

        public override bool IsRight => false;

        public override Either<TLeft, TRight> Do(Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            if (leftAct == null)
            {
                throw new ArgumentNullException(nameof(leftAct));
            }

            leftAct(_value);
            return this;
        }

        public override T Do<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc) => leftFunc != null ? leftFunc(_value) : throw new ArgumentNullException(nameof(leftFunc));

        public override TLeft Left() => _value;

        public override T Left<T>(Func<TLeft, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public override Either<TLeft, TRight> Left(Action<TLeft> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public override TRight Right() => throw new InvalidOperationException("A left-sided either does not contain a right value");

        public override T Right<T>(Func<TRight, T> func) => default(T);

        public override Either<TLeft, TRight> Right(Action<TRight> action) => this;
    }
}