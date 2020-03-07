using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class NewspaperModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string Issn { get; set; }
    }
}