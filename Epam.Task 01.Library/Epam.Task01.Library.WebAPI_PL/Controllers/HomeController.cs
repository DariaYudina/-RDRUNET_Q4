using System.Web.Mvc;
using Epam.Task01.Library.WebAPI_PL.Filters;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [LogErrorAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
