using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    public class homeController : Controller
    {
        public ActionResult momencraft()
        {
            ViewBag.Title = "MomenCraft";

            return View();
        }

        public ActionResult momenpic()
        {
            ViewBag.Title = "Momenpic";

            return View();
        }

        public ActionResult momentic()
        {
            ViewBag.Title = "Momentic";

            return View();
        }
    }
}
