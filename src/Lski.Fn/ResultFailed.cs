using System;

namespace Lski.Fn
{
    internal class ResultFailed : Result
    {
        public override Error Error { get; }

        public ResultFailed(string message) : base(false)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "An unsuccesful result requires an error message");
            }

            Error = new Error(message);
        }

        public ResultFailed(Error error) : base(false)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error), "An unsuccesful result requires an error");
            }

            Error = error;
        }
    }
}