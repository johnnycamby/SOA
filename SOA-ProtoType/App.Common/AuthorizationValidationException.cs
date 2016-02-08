using System;

namespace App.Common
{
    public class AuthorizationValidationException : ApplicationException
    {

        public AuthorizationValidationException(string message): base(message , null)
        {}

        public AuthorizationValidationException(string message, Exception innerException):base(message, innerException)
        {}
         
    }
}