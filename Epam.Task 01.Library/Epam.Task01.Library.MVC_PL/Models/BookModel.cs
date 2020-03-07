using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PagesCount { get; set; }

        public string Commentary { get; set; }

        public virtual int YearOfPublishing { get; set; }

        public List<AuthorModel> Authors { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string isbn { get; set; }
    }
}