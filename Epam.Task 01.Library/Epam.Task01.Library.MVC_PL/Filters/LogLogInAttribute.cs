using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Filters
{
    public class LogLogInAttribute : ActionFilterAttribute, IActionFilter
    {

        public ILoggerDao Logger { get; set; }

        public LogLogInAttribute()
        {
            Logger = DependencyResolver.Current.GetService<ILoggerDao>();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var tempData = filterContext.Controller.TempData;
            if ((bool)tempData["IsAuth"] && tempData.ContainsKey("Login"))
            {
                LogDetail logDetail = new LogDetail()
                {
                    ControllerName = filterContext.RouteData.Values["controller"].ToString() + "Controller",
                    ActionName = filterContext.RouteData.Values["action"].ToString(),
                    Date = DateTime.UtcNow,
                    Login = (string)tempData["Login"],
                    AppLayer = "PL",
                    Message = $"User: \"{(string)tempData["Login"]}\" logged in."
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