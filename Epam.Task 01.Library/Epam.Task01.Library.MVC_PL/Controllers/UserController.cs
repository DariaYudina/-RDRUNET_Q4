using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Epam.Task01.Library.MVC_PL.Filters;

namespace Epam.Task01.Library.MVC_PL.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        public UserController(IMapper mapper, IUserLogic userLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogLogIn]
        public ActionResult LogIn(LogInModel logInModel)
        {
            if (_userLogic.VerifyUser(logInModel.Login, logInModel.Password))
            {
                TempData["Login"] = logInModel.Login;
                TempData["IsAuth"] = true;
                FormsAuthentication.SetAuthCookie(logInModel.Login, true);
                return Redirect("~");
            }
            else
            {
                return View(logInModel);
            }
        }

        public ActionResult SignUp()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogRegistration]
        public ActionResult SignUp(SignUpModel signupModel)
        {
            if (ModelState.IsValid)
            {
                ValidationObject validationObject = new ValidationObject();

                if (validationObject.IsValid && _userLogic.AddUser(out validationObject, _mapper.Map<User>(signupModel)))
                {
                    TempData["Login"] = signupModel.Login;
                    TempData["IsAuth"] = true;
                    FormsAuthentication.SetAuthCookie(signupModel.Login, true);
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
                    return View(signupModel);
                }
            }
            return View(signupModel);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult AuntificationReference()
        {
            if(User.Identity.IsAuthenticated)
            {
                return PartialView("_UserIsAuntificatedPartial");
            }
            else
            {
                return PartialView("_UserIsNotAuntificatedPartial");
            }
        }
    }
}