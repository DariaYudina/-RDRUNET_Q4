using Epam.Task01.Library.WebAPI_PL.Filters;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.WebAPI_PL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
