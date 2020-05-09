using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Epam.Task01.Library.WebAPI_PL.Filters
{
    public class LogAuthenticationUserAttribute : ActionFilterAttribute
    {
        [Inject]
        public ILoggerDao Logger { get; set; }

        public Type ExceptionType { get; set; }
        public string Message { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var user = actionExecutedContext.ActionContext.RequestContext.Principal.Identity;
            if (user.IsAuthenticated && !string.IsNullOrEmpty(user.Name) && !string.IsNullOrWhiteSpace(user.Name))
            {
                LogDetail logDetail = new LogDetail()
                {
                    ControllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                    ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                    Date = DateTime.UtcNow,
                    Login = user.Name,
                    AppLayer = "PL",
                    Message = $"User: \"{user.Name}\" logged in"
                };

                Logger.LogError(logDetail);
            }
        }
    }
}