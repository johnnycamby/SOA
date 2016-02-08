using System;

namespace App.Common
{
    public class DeveloperNotHiredException : ApplicationException
    {
        public DeveloperNotHiredException(string message):base(message)
        {}

        public DeveloperNotHiredException(string message, Exception ex):base(message, ex)
        {}
    }
}