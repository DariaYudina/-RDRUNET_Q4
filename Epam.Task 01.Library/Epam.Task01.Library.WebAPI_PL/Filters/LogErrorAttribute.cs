using System;
using System.Diagnostics;
using System.Net;
using System.Web.Http.Filters;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Ninject;

namespace Epam.Task01.Library.WebAPI_PL.Filters
{
    public class LogErrorAttribute : ExceptionFilterAttribute
    {
        [Inject]
        public ILoggerDao Logger { get; set; }

        public Type ExceptionType { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //if (actionExecutedContext.Exception.InnerException is AppLayerException)
            //{
            //    var stacktraceMethod = new StackTrace(actionExecutedContext.Exception.InnerException).GetFrame(0).GetMethod();
            //    LogDetail logDetail = new LogDetail()
            //    {
            //        Message = actionExecutedContext.Exception.InnerException.Message,
            //        StackTrace = actionExecutedContext.Exception.InnerException.StackTrace,
            //        ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
            //        Date = DateTime.UtcNow,
            //        AppLayer = ((AppLayerException)actionExecutedContext.Exception.InnerException).AppLayer,
            //        MethodName = stacktraceMethod.Name,
            //        ClassName = stacktraceMethod.DeclaringType.Name
            //    };

            //    Logger.LogError(logDetail);
            //}
            if (actionExecutedContext.Exception != null)
            {
                var stacktraceMethod = new StackTrace(actionExecutedContext.Exception).GetFrame(0).GetMethod();
                LogDetail logDetail = new LogDetail()
                {
                    Message = actionExecutedContext.Exception.Message,
                    StackTrace = actionExecutedContext.Exception.StackTrace,
                    ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                    Date = DateTime.UtcNow,
                    AppLayer = ((AppLayerException)actionExecutedContext.Exception).AppLayer,
                    MethodName = stacktraceMethod.Name,
                    ClassName = stacktraceMethod.DeclaringType.Name,
                    Login = actionExecutedContext.ActionContext.Request.RequestUri.UserInfo ?? "Guest"

                };

                Logger.LogError(logDetail);
            }
        }

        //public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext,
        //    CancellationToken cancellationToken)
        //{
        //    if (actionExecutedContext.Exception != null
        //    && actionExecutedContext.Exception.GetType() == ExceptionType)
        //    {
        //        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(StatusCode, Message);
        //    }
        //    return Task.FromResult<object>(null);
        //}
    }
}