using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Epam.Task01.Library.MVC_PL.Models;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class DisplayBookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PagesCount { get; set; }

        [DisplayFormat(NullDisplayText = "not defined")]
        public string Commentary { get; set; }

        public virtual int YearOfPublishing { get; set; }

        public List<DisplayAuthorModel> Authors { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        [DisplayFormat(NullDisplayText = "not defined")]
        public string Isbn { get; set; }

        public override string ToString()
        {
            return Authors != null ? string.Join(", ", Authors) : "not defined";
        }
    }
}