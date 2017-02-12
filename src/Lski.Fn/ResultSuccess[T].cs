using System;

namespace Lski.Fn
{
    internal class ResultSuccess<T> : Result<T>
    {
        private T _data;

        public override T Value => _data;

        public ResultSuccess(T data) : base(true)
        {
            if (data == null)
            {
                throw new ArgumentNullException("A success should not be null");
            }

            _data = data;
        }
    }
}