using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XplicitApp.Controllers.Base;

namespace XplicitApp.Controllers.MVC
{
    public class AccountController : XplicitControllerBase
    {
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public ActionResult LocalLogout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}