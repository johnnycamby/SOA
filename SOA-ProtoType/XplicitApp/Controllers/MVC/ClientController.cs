using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel.Client;
using XplicitApp.Infrastracture.ActionResults;
using XplicitApp.Infrastracture.Utils;
using XplicitConstants;

namespace XplicitApp.Controllers.MVC
{
    [Authorize]
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("client/account")]
        public ActionResult PersonalAccount()
        {
            return View();
        }

        [HttpGet]
        [Route("client/book")]
        public ActionResult BookDeveloper()
        {
            return View();
        }

        [HttpGet]
        [Route("client/bookings")]
        public ActionResult CurrentBookings()
        {
            return View();
        }

        [HttpGet]
        [Route("client/history")]
        public JsonResult HiringHistory()
        {
            return new AwesomeJsonResult();
        }

        //public async Task<ActionResult> Index()
        //{

        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        var identity = this.User.Identity as ClaimsIdentity;
        //        if (identity != null)
        //            foreach (var claim in identity.Claims)
        //            {
        //                Debug.WriteLine(claim.Type + " - " + claim.Value);
        //            }
        //    }

        //    var httpClient = DeveloperHttpClient.GetClient();

        //    return null;
        //}

        

        //public async Task<ActionResult> DeveloperInfo(Guid developerId)
        //{

        //    var token = (User.Identity as ClaimsIdentity).FindFirst("access_token").Value;
        //    var userInfoClient = new UserInfoClient(new Uri(Constants.XplicitStsUserInfoEndpoint), token);
        //    var userInfoResponse = await userInfoClient.GetAsync();

        //    if (!userInfoResponse.IsError)
        //    {
        //        dynamic addressInfo = new ExpandoObject();
        //        addressInfo.Address = userInfoResponse.Claims.First(c => c.Item1 == "address").Item2;

        //        return View(addressInfo);
        //    }
        //    else
        //    {
        //      var    exception = new Exception("Problem getting address. Please contact systems-admin");
        //        return View("Error", new HandleErrorInfo(exception, "Client", "DeveloperInfo"));
        //    }
        //} 
    }
}