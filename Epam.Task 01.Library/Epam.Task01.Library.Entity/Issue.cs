using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Issue
    {

        public Issue(string title, string city, string publishingCompany, string issn)
        {
            Title = title;
            City = city;
            PublishingCompany = publishingCompany;
            Issn = issn;
        }

        public int IssueId { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string Issn { get; set; }
    }
}
