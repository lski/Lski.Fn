using System;

namespace Lski.Fn
{
    public class Error
    {
        private readonly string _message;

        public Error(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "An error object requires an error message");
            }

            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        public static implicit operator string(Error err)
        {
            return err._message;
        }
    }
}