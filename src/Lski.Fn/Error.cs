namespace Lski.Fn
{
    public class Error
    {
        private readonly string _message;

        public Error(string message)
        {
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