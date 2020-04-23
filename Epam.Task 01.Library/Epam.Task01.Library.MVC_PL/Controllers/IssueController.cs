using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "User")]
    public class IssueController : Controller
    {
        private readonly IIssueLogic _issueLogic;
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;
        private readonly INewspaperLogic _newspaperLogic;
        private readonly IAuthorLogic _authorLogic;

        public IssueController(IMapper mapper, ICommonLogic commonLogic, IIssueLogic issueLogic, INewspaperLogic newspaperLogic, IAuthorLogic authorLogic)
        {
            _issueLogic = issueLogic;
            _mapper = mapper;
            _commonLogic = commonLogic;
            _newspaperLogic = newspaperLogic;
            _authorLogic = authorLogic;
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create()
        {
            var newspapers = _newspaperLogic.GetNewspapers().Select(c => new {
                Id = c.Id,
                Name = c.Title
            }).ToList();
            ViewBag.Newspapers = new SelectList(newspapers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Create(CreateIssueModel issueModel)
        {
            var newspapers = _newspaperLogic.GetNewspapers().Select(c => new {
                c.Id,
                Name = c.Title
            }).ToList();
            ViewBag.Newspapers = new SelectList(newspapers, "Id", "Name");

            if (ModelState.IsValid)
            {
                issueModel.Newspaper = _newspaperLogic.GetNewspaperById(issueModel.NewspaperId);
                ValidationObject validationObject = new ValidationObject();              
                if (validationObject.IsValid && _issueLogic.AddIssue(out validationObject, _mapper.Map<Issue>(issueModel)))
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
                    return View(issueModel);
                }
            }
            return View(issueModel);
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
            var model = _mapper.Map<DisplayIssueModel>(item);
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

            var model = _mapper.Map<EditIssueModel>(item);
            var newspapers = _newspaperLogic.GetNewspapers().Select(c => new {
                c.Id,
                Name = c.Title
            }).ToList();
            model.NewspaperSelectList = new SelectList(newspapers, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Edit(EditIssueModel issueModel)
        {
            var newspapers = _newspaperLogic.GetNewspapers().Select(c => new {
                c.Id,
                Name = c.Title
            }).ToList();
            ViewBag.Newspapers = new SelectList(newspapers, "Id", "Name");

            if (ModelState.IsValid)
            {
                issueModel.Newspaper = _newspaperLogic.GetNewspaperById(issueModel.NewspaperId);
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid && _issueLogic.EditIssue(out validationObject, _mapper.Map<Issue>(issueModel)))
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
                    return View(issueModel);
                }
            }
            return View(issueModel);
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

            var model = _mapper.Map<DisplayIssueModel>(item);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetIssuesByNewspaper(int newspaperId, int currentIssueId)
        {
            var items = _issueLogic.GetIssuesByNewspaperId(newspaperId, currentIssueId).ToList();
            var model = _mapper.Map<IEnumerable<IssuePublicationModel>>(items);
            if (items.Count != 0)
            {
                return PartialView("_IssuesByNewspaper", model);
            }

            return null;
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniqueIssue(int newspaperId, DateTime dateOfPublishing)
        {
            var newspaper = _newspaperLogic.GetNewspaperById(newspaperId);
            var issues = _issueLogic.GetIssues();
            bool notUniq = false;
            if (dateOfPublishing != null && newspaperId != 0)
            {
                notUniq = (issues.FirstOrDefault(i =>
                string.Equals(i.Title, newspaper.Title, StringComparison.InvariantCultureIgnoreCase)
                && i.DateOfPublishing == dateOfPublishing
                && string.Equals(i.Newspaper.PublishingCompany, newspaper.PublishingCompany, StringComparison.InvariantCultureIgnoreCase)))
                != null;
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniqueIssueEdit(int id, int newspaperId, DateTime dateOfPublishing)
        {
            var newspaper = _newspaperLogic.GetNewspaperById(newspaperId);
            var issues = _issueLogic.GetIssues().Where(i => i.Id != id);
            bool notUniq = false;
            if (dateOfPublishing != null && newspaperId != 0)
            {
                notUniq = (issues.FirstOrDefault(i =>
                string.Equals(i.Title, newspaper.Title, StringComparison.InvariantCultureIgnoreCase)
                && i.DateOfPublishing == dateOfPublishing
                && string.Equals(i.Newspaper.PublishingCompany, newspaper.PublishingCompany, StringComparison.InvariantCultureIgnoreCase)))
                != null;
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }
    }
}