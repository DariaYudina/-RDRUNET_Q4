using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.Task01.Library.MVC_PL.Models;

namespace Epam.Task01.Library.MVC_PL.ViewModels
{
    public class AllLibraryItemsModel
    {
        public List<DisplayLibraryItemModel> LibraryItems { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<int> authorsId { get; set; }
    }
}