using System;

namespace Lski.Fn
{
    public interface IEither<TLeft, TRight>
    {
        T Do<T>(Func<TLeft, T> left, Func<TRight, T> right);

        IEither<TLeft, TRight> Do(Action<TLeft> left, Action<TRight> right);
    }
}