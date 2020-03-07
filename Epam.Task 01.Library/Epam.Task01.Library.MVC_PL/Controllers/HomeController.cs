using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.MVC_PL.ViewModels.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    public class HomeController : Controller
    {
        private IBookLogic _bookLogic;
        public HomeController(IBookLogic bookLogic)
        {
            _bookLogic = bookLogic;
        }
        public ActionResult Index()
        {
            var model = _bookLogic.GetBookItems().Select( book => new DisplayBookModel() { Title = book.Title, City = book.City,
                Commentary = book.Commentary, isbn = book.isbn, PagesCount = book.PagesCount, PublishingCompany = book.PublishingCompany, 
                YearOfPublishing = book.YearOfPublishing});
            return View(model);
        }
    }
}