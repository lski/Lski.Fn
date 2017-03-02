﻿using System;

namespace Lski.Fn
{
    internal class EitherRight<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TRight _value;

        public EitherRight(TRight value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("An either object should contain a value. Consider using a Maybe<T> if the value is optional");
            }

            _value = value;
        }

        public override bool IsLeft => false;

        public override bool IsRight => true;

        public override Either<TLeft, TRight> LeftOrRight(Action<TLeft> leftAct, Action<TRight> rightAct)
        {
            if (rightAct == null)
            {
                throw new ArgumentNullException(nameof(rightAct));
            }

            rightAct(_value);
            return this;
        }

        public override T LeftOrRight<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc)
        {
            return rightFunc != null ? rightFunc(_value) : throw new ArgumentNullException(nameof(rightFunc));
        }

        public override TRight Right() => _value;

        public override Either<TLeft, TRightOut> Right<TRightOut>(Func<TRight, TRightOut> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return Either.Right<TLeft, TRightOut>(func(_value));
        }

        public override Either<TLeft, TRight> Right(Action<TRight> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public override TLeft Left()
        {
            throw new InvalidOperationException("A right-side either does not contain a left value");
        }

        public override Either<TLeftOut, TRight> Left<TLeftOut>(Func<TLeft, TLeftOut> func)
        {
            return Either.Right<TLeftOut, TRight>(_value);
        }

        public override Either<TLeft, TRight> Left(Action<TLeft> action) => this;

        public override string ToString() => $"{this._value}";
    }
}