using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Welcome()
        {
            ViewBag.Title = "MomenCraft";

            return View();
        }
    }
}
