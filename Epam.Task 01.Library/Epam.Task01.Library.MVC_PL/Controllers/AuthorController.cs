using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "User")]
    public class AuthorController : Controller
    {
        private readonly IAuthorLogic _authorLogic;
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;

        public AuthorController(IMapper mapper, ICommonLogic commonLogic, IAuthorLogic authorLogic)
        {
            _authorLogic = authorLogic;
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
        public ActionResult Create(CreateAuthorModel authorModel)
        {
            if (ModelState.IsValid)
            {
                ValidationObject validationObject = new ValidationObject();
                var author = _mapper.Map<Author>(authorModel);
                if (validationObject.IsValid && _authorLogic.AddAuthor(out validationObject, author))
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { IsValid = true, author.Id, author.FirstName, author.LastName });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
                }
            }
            if (Request.IsAjaxRequest())
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();
                return Json(new { IsValid = false, msg = errorList });
            }
            else
            {
                return View(authorModel);
            }
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

            var model = _mapper.Map<DisplayAuthorModel>(item);
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

            var model = _mapper.Map<DisplayAuthorModel>(item);
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

            var model = _mapper.Map<DisplayAuthorModel>(item);
            return View(model);
        }
    }
}