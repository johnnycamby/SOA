using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XplicitApp.Controllers.Base;

namespace XplicitApp.Controllers.MVC
{
    [RoutePrefix("home")]
    public class HomeController : XplicitControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("personal")]
        [Authorize]
        public ActionResult PersonalAccount()
        {
            return View();
        }
    }
}