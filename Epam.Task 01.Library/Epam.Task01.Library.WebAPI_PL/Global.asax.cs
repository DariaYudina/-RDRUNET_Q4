using AutoMapper;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.WebAPI_PL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Epam.Task01.Library.WebAPI_PL
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
