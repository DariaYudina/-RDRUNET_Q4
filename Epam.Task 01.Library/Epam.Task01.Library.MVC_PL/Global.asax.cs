using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Epam.Task01.Library.MVC_PL.Filters;

namespace Epam.Task01.Library.MVC_PL
{
    public class MvcApplication : HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new LogErrorAttribute());
        }
    }
}
