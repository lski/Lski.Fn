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
                throw new ArgumentNullException("A successful result cant be null");
            }

            _data = data;
        }
    }
}