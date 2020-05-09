using Epam.Task01.Library.WebAPI_PL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
