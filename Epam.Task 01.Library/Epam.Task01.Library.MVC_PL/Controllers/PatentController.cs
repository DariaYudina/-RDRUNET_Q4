using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels.Patents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "User")]
    public class PatentController : Controller
    {
        private readonly IPatentLogic _patentLogic;
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;
        private readonly IAuthorLogic _authorLogic;

        public PatentController(IMapper mapper, ICommonLogic commonLogic, IPatentLogic patentLogic, IAuthorLogic authorLogic)
        {
            _patentLogic = patentLogic;
            _mapper = mapper;
            _commonLogic = commonLogic;
            _authorLogic = authorLogic;
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create()
        {
            var authors = _authorLogic.GetAuthors().Select(c => new {
                AuthorID = c.Id,
                AuthorName = c.FirstName + " " + c.LastName
            }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "AuthorID", "AuthorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create(CreatePatentModel patentModel)
        {
            var authors = _authorLogic.GetAuthors().Select(c => new {
                AuthorID = c.Id,
                AuthorName = c.FirstName + " " + c.LastName
            }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "AuthorID", "AuthorName");

            if (ModelState.IsValid)
            {
                foreach (int id in patentModel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        patentModel.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid && _patentLogic.AddPatent(out validationObject, _mapper.Map<Patent>(patentModel)))
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
                    return View(patentModel);
                }
            }
            return View(patentModel);
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpNotFoundResult();
            }

            var item = _commonLogic.GetLibraryItemById(id);
            if (item == null)
            {
                return new HttpNotFoundResult();
            }

            var model = _mapper.Map<DisplayPatentModel>(item);
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

            var model = _mapper.Map<EditPatentModel>(item);
            var authors = _authorLogic.GetAuthors().Select(c => new {
                c.Id,
                Name = c.FirstName + " " + c.LastName,
            }).ToList();
            model.AuthorsId = model.Authors.Select(i => i.Id).ToArray();
            model.AuthorsSelectList = new SelectList(authors, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Edit(EditPatentModel patentModel)
        {
            var authors = _authorLogic.GetAuthors().Select(c => new {
                AuthorID = c.Id,
                AuthorName = c.FirstName + " " + c.LastName
            }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "AuthorID", "AuthorName");

            if (ModelState.IsValid)
            {
                foreach (int id in patentModel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        patentModel.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid && _patentLogic.EditPatent(out validationObject, _mapper.Map<Patent>(patentModel)))
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
                    return View(patentModel);
                }
            }
            return View(patentModel);
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

            var model = _mapper.Map<DisplayPatentModel>(item);
            return View(model);
        }


        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniquePatent(string registrationNumber, string country)
        {
            var patents = _patentLogic.GetPatents();
            bool notUniq = false;
            if (registrationNumber != null && country != null)
            {
                notUniq = patents.Any(i => string.Equals(i.RegistrationNumber, registrationNumber, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(i.Country, country, StringComparison.InvariantCultureIgnoreCase));
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniquePatentEdit(int id, string registrationNumber, string country)
        {
            var patents = _patentLogic.GetPatents().Where(i => i.Id != id);
            bool notUniq = false;
            if (registrationNumber != null && country != null)
            {
                notUniq = patents.Any(i => string.Equals(i.RegistrationNumber, registrationNumber, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(i.Country, country, StringComparison.InvariantCultureIgnoreCase));
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }

    }
}