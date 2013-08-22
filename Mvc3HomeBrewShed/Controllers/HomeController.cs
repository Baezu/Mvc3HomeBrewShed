using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc3HomeBrewShed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Home Brew Journal";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
