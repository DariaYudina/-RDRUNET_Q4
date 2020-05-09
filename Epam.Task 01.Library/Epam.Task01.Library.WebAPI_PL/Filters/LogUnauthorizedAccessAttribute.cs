using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Epam.Task01.Library.WebAPI_PL.Filters
{
    public class LogUnauthorizedAccessAttribute : Attribute, IAuthorizationFilter
    {
        private string role;
        public LogUnauthorizedAccessAttribute(string role)
        {
            this.role = role;
        }

        [Inject]
        public ILoggerDao Logger { get; set; }

        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IPrincipal principal = actionContext.RequestContext.Principal;
            if (principal == null || !principal.IsInRole(role))
            {
                var user = actionContext.RequestContext.Principal.Identity.Name ?? "Guest";
                LogDetail logDetail = new LogDetail()
                {
                    ControllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName,
                    ActionName = actionContext.ActionDescriptor.ActionName,
                    Date = DateTime.UtcNow,
                    Login = user,
                    AppLayer = "PL",
                    Message = $"User: \"{user}\" was attempted unauthorized access to closed functionality."
                };

                Logger.LogError(logDetail);

                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse<string>(HttpStatusCode.Unauthorized, "Please Login"));
            }
            else
            {
                return continuation();
            }
        }
    }
}