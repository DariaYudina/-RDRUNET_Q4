using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.LibraryItems
{
    public class WebApiLibraryItemsPagesModels
    {
        public int TotalPages { get; set; }

        public int CurPage { get; set; }

        public IEnumerable<WebApiLibraryItemsModel> LibraryItem { get; set; }

        public WebApiLibraryItemsPagesModels()
        {
            this.LibraryItem = new List<WebApiLibraryItemsModel>();
        }
    }
}