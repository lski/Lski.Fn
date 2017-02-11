namespace Lski.Fn
{ 
    internal class ResultFailed : Result
    {
        private string _error;

        public override string Error => _error;

        public ResultFailed(string error) : base(false)
        {
            _error = error;
        }
    }
}