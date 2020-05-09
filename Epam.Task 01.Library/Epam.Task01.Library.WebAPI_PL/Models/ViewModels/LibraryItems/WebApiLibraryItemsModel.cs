using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.LibraryItems
{
    public class WebApiLibraryItemsModel
    {
        public string Type { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public int PagesCount { get; set; }
    }
}