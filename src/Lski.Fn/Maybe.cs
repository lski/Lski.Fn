using System;

namespace Lski.Fn
{
    public static class Maybe
    {
        public static Maybe<T> Create<T>(T value) => new Maybe<T>(value);

        public static Maybe<T> ToMaybe<T>(this T value) => new Maybe<T>(value);
    }
}