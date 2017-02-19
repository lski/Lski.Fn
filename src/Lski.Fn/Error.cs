using System;

namespace Lski.Fn
{
    ///<summary>
    /// Represents an Failed message from a Result
    ///</summary>
    public class Error
    {
        private readonly string _message;

        ///<summary>
        /// Represents an Failed message from a Result
        ///</summary>
        public Error(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "An error object requires an error message");
            }

            _message = message;
        }

        ///<summary>
        /// Returns the error message of this Failure 
        ///</summary>
        public override string ToString() => _message;

        ///<summary>
        /// Converts an failure object to a string object
        ///</summary>
        public static implicit operator string(Error err) => err._message;
        
        ///<summary>
        /// Converts an failure object to a string object
        ///</summary>
        public static implicit operator Error(string err) => new Error(err);
    }
}