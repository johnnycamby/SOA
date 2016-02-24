using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using XplicitApp.Infrastracture.ActionResults;

namespace XplicitApp.Controllers.Base
{
    public abstract class XplicitControllerBase: Controller
    {

        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController: Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        // Takes in a model and returns an AwesomeJson result containing that model
        public AwesomeJsonResult<T> AwesomeJson<T>(T model)
        {
            return new AwesomeJsonResult<T>() {Data = model};
        }

        // send back model validation errors
        public AwesomeJsonResult JsonValidationError()
        {
            var result = new AwesomeJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }

            return result;
        }

        public AwesomeJsonResult JsonError(string errorMessage)
        {
            var result = new AwesomeJsonResult();
            result.AddError(errorMessage);

            return result;
        }

    }
}