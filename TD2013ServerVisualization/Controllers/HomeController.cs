using System.Web.Mvc;

namespace TD2013ServerVisualization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Server side data visualization (hubs, broadcast)";
            return View();
        }
    }
}
