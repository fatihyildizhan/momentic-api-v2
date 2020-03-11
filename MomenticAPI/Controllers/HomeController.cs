using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    public class homeController : Controller
    {
        public ActionResult momencraft()
        {
            if (Request.Browser.IsMobileDevice)
            {
                 Response.RedirectToRoute("home", "m");
            }

            ViewBag.Title = "MomenCraft";
            return View();
        }
    }
}
