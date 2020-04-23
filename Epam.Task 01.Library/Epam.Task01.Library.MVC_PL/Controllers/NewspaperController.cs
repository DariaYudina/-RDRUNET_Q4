using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels.Newspapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "User")]
    public class NewspaperController : Controller
    {
        private readonly INewspaperLogic _newspaperLogic;
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;

        public NewspaperController(IMapper mapper, ICommonLogic commonLogic, INewspaperLogic newspaperLogic)
        {
            _newspaperLogic = newspaperLogic;
            _mapper = mapper;
            _commonLogic = commonLogic;
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create(CreateNewspaperModel newspaperModel)
        {
            if (ModelState.IsValid)
            {
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid && _newspaperLogic.AddNewspaper(out validationObject, _mapper.Map<Newspaper>(newspaperModel)))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var i in validationObject.ValidationExceptions)
                    {
                        if (ModelState.ContainsKey(i.Property))
                        {
                            ModelState.AddModelError(i.Property, i.Message);
                        }
                        else
                        {
                            ModelState.AddModelError("", i.Message);
                        }
                    }
                    return View(newspaperModel);
                }
            }
            return View(newspaperModel);
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpNotFoundResult();
            }

            var item = _commonLogic.GetLibraryItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return new HttpNotFoundResult();
            }

            var model = _mapper.Map<DisplayNewspaperModel>(item);
            return View(model);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return new HttpNotFoundResult();
            }

            var item = _commonLogic.GetLibraryItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return new HttpNotFoundResult();
            }

            var model = _mapper.Map<DisplayNewspaperModel>(item);
            return View(model);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return new HttpNotFoundResult();
            }

            var item = _commonLogic.GetLibraryItems().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return new HttpNotFoundResult();
            }

            var model = _mapper.Map<DisplayNewspaperModel>(item);
            return View(model);
        }


        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniqueNewspaper(string title, string issn)
        {
            var newspapers = _newspaperLogic.GetNewspapers();
            bool notUniq = false;
            if (issn != null && title!= null)
            {
                notUniq = newspapers.Any(i => string.Equals(i.Title, title, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(i.Issn, issn, StringComparison.InvariantCultureIgnoreCase));
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }
    }
}