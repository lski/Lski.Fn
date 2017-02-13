namespace Lski.Fn
{ 
    internal class ResultFailed : Result
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