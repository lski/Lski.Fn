namespace Lski.Fn
{
    internal class ResultFailed<T> : Result<T>
    {
        public override Error Error { get; }

        public ResultFailed(string error) : base(false)
        {
            Error = new Error(error);
        }

        public ResultFailed(Error error) : base(false)
        {
            Error = error;
        }
    }
}