using System;

namespace Lski.Fn
{
    ///<summary>
    /// Represents an Failed message from a Result
    ///</summary>
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

        ///<summary>
        /// Returns the error message of this Failure 
        ///</summary>
        public override string ToString()
        {
            return _message;
        }

        ///<summary>
        /// Converts an failure object to a string object
        ///</summary>
        public static implicit operator string(Error err)
        {
            return err._message;
        }
    }
}