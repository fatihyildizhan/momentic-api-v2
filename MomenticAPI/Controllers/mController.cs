using System.Web.Mvc;

namespace MomenticAPI.Controllers
{
    public class mController : Controller
    {
        public ActionResult home()
        {
            return View();
        }
    }
}