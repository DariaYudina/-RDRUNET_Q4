using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Newspapers
{
    public class WebApiNewspaperModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string Issn { get; set; }

        public IEnumerable<WebApiIssueWithoutNewspaperModel> Issues { get; set; }
    }
}