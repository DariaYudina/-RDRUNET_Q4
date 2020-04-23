using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.MVC_PL.Filters;
using Epam.Task01.Library.MVC_PL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    [LogUnauthorizedAccessAttempt(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        public AdminController(IMapper mapper, IUserLogic userLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
        }

        public ActionResult UsersDetails()
        {
            return View(_mapper.Map<List<UserDetailsModel>>(_userLogic.GetUsers()));
        }

        public ActionResult EditRoles(int userid = 0)
        {
            if (userid == 0)
            {
                return new HttpNotFoundResult();
            }

            var item = _userLogic.GetUsers().FirstOrDefault(i => i.Id == userid);
            if (item == null)
            {
                return new HttpNotFoundResult();
            }

            var roles = _userLogic.GetRoles().Select(c => new {
                RoleID = c.Id,
                RoleName = c.RoleName
            }).ToList();
            ViewBag.Roles = new MultiSelectList(roles, "RoleID", "RoleName");

            var model = _mapper.Map<UserEditModel>(item);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoles(UserEditModel user)
        {
            var roles = _userLogic.GetRoles().Select(c => new {
                RoleID = c.Id,
                RoleName = c.RoleName
            }).ToList();
            ViewBag.Roles = new MultiSelectList(roles, "RoleID", "RoleName");

            if (_userLogic.ChangeUserRoles(user.Id, user.RolesId))
            {
                return RedirectToAction("Index", "Home", null);
            }
            var model = _mapper.Map<UserEditModel>(user);
            return View(model);
        }
    }
}