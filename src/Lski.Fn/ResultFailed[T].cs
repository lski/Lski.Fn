namespace Lski.Fn
{
    internal class ResultFailed<T> : Result<T>
    {
        private string _error;

        public override string Error => _error;

        public ResultFailed(string error) : base(false)
        {
            _error = error;
        }
    }
}