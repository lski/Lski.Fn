using System.Diagnostics;

namespace Lski.Fn
{
    internal class ResultSuccess : Result
    {
        public ResultSuccess() : base(true)
        {
        }

        /// <summary>
        /// Returns the "ToString" of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override string ToString() => "Success";

        /// <summary>
        /// Get the hash code of the underlying value
        /// </summary>
        [DebuggerStepThrough]
        public override int GetHashCode() => 1;
    }
}