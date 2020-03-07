using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper
    {
        public Newspaper() { }

        public Newspaper(string title, string city, string publishingCompany, string issn)
        {
            Title = title;
            City = city;
            PublishingCompany = publishingCompany;
            Issn = issn;
        }

        public Newspaper(int id, string title, string city, string publishingCompany, string issn)
        {
            Id = id;
            Title = title;
            City = city;
            PublishingCompany = publishingCompany;
            Issn = issn;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string Issn { get; set; }
    }
}
