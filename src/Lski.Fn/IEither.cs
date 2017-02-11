using System;

namespace Lski.Fn
{
    public interface IEither<TFirst, TSecond>
    {
        T Do<T>(Func<TFirst, T> first, Func<TSecond, T> second);

        IEither<TFirst, TSecond> Do(Action<TFirst> first, Action<TSecond> second);
    }
}