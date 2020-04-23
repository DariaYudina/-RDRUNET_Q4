using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Filters
{
    public class LogErrorAttribute : HandleErrorAttribute, IExceptionFilter
    {
        public ILoggerDao Logger { get; set; }

        public LogErrorAttribute()
        {
            Logger = DependencyResolver.Current.GetService<ILoggerDao>();
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.InnerException is AppLayerException)
            {
                var stacktraceMethod = new StackTrace(filterContext.Exception.InnerException).GetFrame(0).GetMethod();
                LogDetail logDetail = new LogDetail()
                {
                    Message = filterContext.Exception.InnerException.Message,
                    StackTrace = filterContext.Exception.InnerException.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString() + "Controller",
                    ActionName = filterContext.RouteData.Values["action"].ToString(),
                    Date = DateTime.UtcNow,
                    AppLayer = ((AppLayerException)filterContext.Exception.InnerException).AppLayer,
                    MethodName = stacktraceMethod.Name,
                    ClassName = stacktraceMethod.DeclaringType.Name
                };

                Logger.LogError(logDetail);
                filterContext.ExceptionHandled = true;
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}
