using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Filters
{
    public class LogUnauthorizedAccessAttemptAttribute : AuthorizeAttribute
    {
        public ILoggerDao Logger { get; set; }

        public LogUnauthorizedAccessAttemptAttribute()
        {
            Logger = DependencyResolver.Current.GetService<ILoggerDao>();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var user = filterContext.HttpContext.User;
                var login = user.Identity.Name;
                LogDetail logDetail = new LogDetail()
                {
                    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller",
                    ActionName = filterContext.ActionDescriptor.ActionName,
                    Date = DateTime.UtcNow,
                    Login = login,
                    AppLayer = "PL",
                    Message = $"User: \"{login}\" was attempted unauthorized access to closed functionality."
                };

                Logger.LogError(logDetail);
                filterContext.Result = new HttpStatusCodeResult(403);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}