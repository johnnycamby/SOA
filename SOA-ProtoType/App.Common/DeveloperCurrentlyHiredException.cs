using System;

namespace App.Common
{
    public class DeveloperCurrentlyHiredException : ApplicationException
    {
        public DeveloperCurrentlyHiredException(string message):base(message)
        {}

        public DeveloperCurrentlyHiredException(string message, Exception ex):base(message, ex)
        {}
    }
}