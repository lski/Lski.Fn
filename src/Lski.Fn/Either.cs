namespace Lski.Fn
{
    public static class Either
    {
        public static IEither<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => new EitherLeft<TLeft, TRight>(value);

        public static IEither<TLeft, TRight> Right<TLeft, TRight>(TRight value) => new EitherRight<TLeft, TRight>(value);

        public static IEither<TLeft, TRight> ToLeft<TLeft, TRight>(this TLeft value) => new EitherLeft<TLeft, TRight>(value);

        public static IEither<TLeft, TRight> ToRight<TLeft, TRight>(this TRight value) => new EitherRight<TLeft, TRight>(value);
    }
}