using System;
using System.Net;
using System.Net.Http;

namespace XplicitApp.Infrastracture.Utils
{
    public static class ExceptionHelper
    {
        public static Exception GetExceptionFromResponse(HttpResponseMessage responseMessage)
        {

            // unauthorized => missing/bad auth
            // forbidden => you're authenticated, but you can't do this
            if (responseMessage.StatusCode == HttpStatusCode.Forbidden || responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new Exception("You're not allowed to do that.");
            }
            else
            {
                return new Exception("Something went wrong - please contact systems-admin. ");
            }
        }
    }
}