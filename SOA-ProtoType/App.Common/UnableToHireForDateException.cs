using System;

namespace App.Common
{
    public class UnableToHireForDateException : ApplicationException
    {
        public UnableToHireForDateException(string message):base(message)
        {}

        public UnableToHireForDateException(string message, Exception ex):base(message, ex)
        {}
    }
}