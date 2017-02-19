using System;
using System.Threading.Tasks;

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

        public bool IsLeft => true;

        public bool IsRight => false;

        public T Do<T>(Func<TLeft, T> left, Func<TRight, T> right) => left != null ? left(_value) : throw new ArgumentNullException(nameof(left));

        // public async Task<T> Do<T>(Func<Task<TLeft>, T> left, Func<Task<TRight>, T> right)
        // {
        //     if (left == null)
        //     {
        //         throw new ArgumentNullException(nameof(left));
        //     }

        //     var val = await left(_value)
        // }

        public TLeft Left() => _value;

        public T Left<T>(Func<TLeft, T> func) => func != null ? func(_value) : throw new ArgumentNullException(nameof(func));

        public IEither<TLeft, TRight> Left(Action<TLeft> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            action(_value);
            return this;
        }

        public TRight Right() => throw new InvalidOperationException("A left-sided either does not contain a right value");

        public T Right<T>(Func<TRight, T> func) => default(T);

        public IEither<TLeft, TRight> Right(Action<TRight> action) => this;

    }
}