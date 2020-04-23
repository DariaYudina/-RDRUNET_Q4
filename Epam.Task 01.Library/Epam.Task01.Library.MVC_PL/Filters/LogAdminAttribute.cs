using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Filters
{
    public class LogAdminAttribute : IActionFilter
    {
        public ILoggerDao Logger { get; set; }

        public LogAdminAttribute()
        {
            Logger = DependencyResolver.Current.GetService<ILoggerDao>();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            var login = user.Identity.IsAuthenticated 
                    ? user.Identity.Name
                    : "Anonimus user";
            if(user.Identity.IsAuthenticated && user.IsInRole("Admin"))
            {
                LogDetail logDetail = new LogDetail()
                {
                    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller",
                    ActionName = filterContext.ActionDescriptor.ActionName,
                    Date = DateTime.UtcNow,
                    Login = login,
                    AppLayer = "PL",
                    Message = $"Admin: \"{login}\" made changes in:{filterContext.HttpContext.Request.RawUrl}."
                };

                Logger.LogError(logDetail);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Not use
        }
    }
}