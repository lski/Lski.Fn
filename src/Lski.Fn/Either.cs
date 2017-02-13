namespace Lski.Fn
{
    public static class Either
    {
        public static IEither<TFirst, TSecond> First<TFirst, TSecond>(TFirst value) => new EitherFirst<TFirst, TSecond>(value);

        public static IEither<TFirst, TSecond> Second<TFirst, TSecond>(TSecond value) => new EitherSecond<TFirst, TSecond>(value);

        public static IEither<TFirst, TSecond> ToFirst<TFirst, TSecond>(this TFirst value) => new EitherFirst<TFirst, TSecond>(value);

        public static IEither<TFirst, TSecond> ToSecond<TFirst, TSecond>(this TSecond value) => new EitherSecond<TFirst, TSecond>(value);
    }
}