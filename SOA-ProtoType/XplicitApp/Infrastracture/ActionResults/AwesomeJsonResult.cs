using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XplicitApp.Infrastracture.Utils;

namespace XplicitApp.Infrastracture.ActionResults
{
    // custom ActionResult to help pass json-data down from a controller
    public class AwesomeJsonResult : JsonResult
    {
         // To handle Errors that l'l be passed down to the client
        public IList<string> ErrorMessages { get; private set; }

        public AwesomeJsonResult()
        {
            ErrorMessages = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            DealWithHeaderInfo(context);
            SerializeData(context.HttpContext.Response);
        }

        protected virtual void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                Data = new
                {
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };

                response.StatusCode = 400;
            }

            if(Data == null)
                return;
            response.Write(Data.ToJson());
        }

        private void DealWithHeaderInfo(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && "GET".Equals(context.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase) )
            {
                throw new InvalidOperationException("Get access is not allowed. Please change the JsonRequestBehavior if you need GET access.");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
        }
    }

    public class AwesomeJsonResult<T> : AwesomeJsonResult
    {
        public new T Data
        {
            get { return (T) base.Data; }
            set { base.Data = value; }
        }
    }
}