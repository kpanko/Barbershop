using System.Web.Mvc;

namespace Barbershop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A code challenge written in ASP.NET MVC.";

            return View();
        }
    }
}