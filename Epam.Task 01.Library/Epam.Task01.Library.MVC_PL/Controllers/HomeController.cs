using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;

        public HomeController(IMapper mapper, ICommonLogic commonLogic)
        {
            _mapper = mapper;
            _commonLogic = commonLogic;
        }

        public ActionResult GetData(int page = 1)
        {
            List<DisplayLibraryItemModel> records = _mapper.Map<List<DisplayLibraryItemModel>>
                (_commonLogic.GetLibraryItems().OrderBy(i => i.Id).ToList());
            int pageSize = 20;
            List<DisplayLibraryItemModel> reocrdsPages = records.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = records.Count };
            AllLibraryItemsModel ivm = new AllLibraryItemsModel { PageInfo = pageInfo, LibraryItems = reocrdsPages };
            return PartialView("_IndexDataPartial", ivm);
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult UsersReference()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return PartialView("_UsersReferencePartial");
            }

            return null;
        }

        [ChildActionOnly]
        public ActionResult CreateReferences()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Librarian")))
            {
                return PartialView("_CreateReferences");
            }

            return null;
        }

        [ChildActionOnly]
        public ActionResult EditDeleteReferences(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Librarian")))
            {
                return PartialView("EditDeleteReferences", id);
            }

            return null;
        }

        [HttpGet]
        [LogUnauthorizedAccessAttempt(Roles = "Admin, Librarian")]
        public void Delete(int id = 0)
        {
            _commonLogic.DeleteLibraryItemById(id);
        }

        [HttpPost]
        public ActionResult Search(string title)
        {
            var page = 1;
            List<DisplayLibraryItemModel> records = _mapper.Map<List<DisplayLibraryItemModel>>
             (_commonLogic.GetLibraryItemsByTitle(title).OrderBy(i => i.Id));
            int pageSize = 20;
            List<DisplayLibraryItemModel> reocrdsPages = records.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = records.Count };
            AllLibraryItemsModel ivm = new AllLibraryItemsModel { PageInfo = pageInfo, LibraryItems = reocrdsPages };
            return PartialView("_IndexDataPartial", ivm);
        }
    }
}