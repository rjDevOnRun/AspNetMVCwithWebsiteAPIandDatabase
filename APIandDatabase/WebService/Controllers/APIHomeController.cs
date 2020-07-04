using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    /*
     * Keep Web API always on and hosted:
     * reference:  https://weblog.west-wid.com/posts/2013/oct/02/use-iis-application-initialization-for-keeping-aspnet-apps-alive
     * 
     * Steps:
     *          - Make a website
     *          - Use Site Name and host name same
     *          - Change 'hosts' file in windows and add site name to 127.0.0.1
     *          - IIS, change application pool, 'Start mode = AlwaysRunning'
     */
    public class APIHomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "API Home Page";

            return View();
        }
    }
}
