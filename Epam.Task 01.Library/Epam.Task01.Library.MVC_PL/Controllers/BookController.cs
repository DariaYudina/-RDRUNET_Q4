using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels;
using Epam.Task01.Library.MVC_PL.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "User")]
    public class BookController : Controller
    {
        private readonly IBookLogic _bookLogic;
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;
        private readonly IAuthorLogic _authorLogic;

        public BookController(IMapper mapper, ICommonLogic commonLogic, IBookLogic bookLogic, IAuthorLogic authorLogic)
        {
            _bookLogic = bookLogic;
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
        public ActionResult Create(CreateBookModel bookModel)
        {
            var authors = _authorLogic.GetAuthors().Select(c => new {
                AuthorID = c.Id,
                AuthorName = c.FirstName + " " + c.LastName
            }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "AuthorID", "AuthorName");

            if (ModelState.IsValid)
            {
                foreach (int id in bookModel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        bookModel.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid && _bookLogic.AddBook(out validationObject, _mapper.Map<Book>(bookModel))) 
                {
                    return RedirectToAction("Index", "Home", null);
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
                    return View(bookModel);
                }
            }

            return View(bookModel);
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

            var model = _mapper.Map<DisplayBookModel>(item);
            return View(model);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public ActionResult Edit(int id = 0)
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
            var model = _mapper.Map<EditBookModel>(item);
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
        public ActionResult Edit(EditBookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                foreach (int id in bookModel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        bookModel.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                var book = _mapper.Map<Book>(bookModel);

                var authors = _authorLogic.GetAuthors().Select(c => new {
                    c.Id,
                    Name = c.FirstName + " " + c.LastName,
                }).ToList();
                bookModel.AuthorsId = bookModel.Authors.Select(i => i.Id).ToArray();
                bookModel.AuthorsSelectList = new SelectList(authors, "Id", "Name");

                if (validationObject.IsValid && _bookLogic.EditBook(out validationObject, _mapper.Map<Book>(bookModel)))
                {
                    return RedirectToAction("Index", "Home", null);
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
                    return View(bookModel);
                }
            }

            return View(bookModel);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniqueBook(string title, int yearofpublishing, string isbn, IEnumerable<int> authorsId)
        {
            var books = _bookLogic.GetBooks().ToList();
            List<Author> authors = new List<Author>();
            foreach (var id in authorsId)
            {
                authors.Add(_authorLogic.GetAuthorById(id));
            }

            bool notUniq = false;
            if (!string.IsNullOrEmpty(isbn))
            {
                notUniq = books.Any(i => i.Isbn == isbn);
            }
            else if (!string.IsNullOrEmpty(title))
            {
                notUniq = books.FirstOrDefault(i => string.Equals(i.Title, title, StringComparison.InvariantCultureIgnoreCase)
                && i.YearOfPublishing == yearofpublishing)?.Authors.Except(authors).Count() == 0;
            }
            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }

        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        [HttpGet]
        public JsonResult IsUniqueBookEdit(string title, int yearofpublishing, string isbn, IEnumerable<int> authorsId, int id)
        {
            var books = _bookLogic.GetBooks().Where(i => i.Id != id).ToList();
            List<Author> authors = new List<Author>();
            foreach (var i in authorsId)
            {
                authors.Add(_authorLogic.GetAuthorById(id));
            }

            bool notUniq = false;
            if (!string.IsNullOrEmpty(isbn))
            {
                notUniq = books.Any(i => i.Isbn == isbn);
            }
            else if (!string.IsNullOrEmpty(title))
            {
                notUniq = books.FirstOrDefault(i => 
                    string.Equals(i.Title, title, StringComparison.InvariantCultureIgnoreCase)
                    && i.YearOfPublishing == yearofpublishing)
                    ?.Authors.Select(x => new { x.Id })
                   .Except(authors.Select(x => new { x.Id })).ToList().Count() == 0;
            }

            return Json(!notUniq, JsonRequestBehavior.AllowGet);
        }
    }
}