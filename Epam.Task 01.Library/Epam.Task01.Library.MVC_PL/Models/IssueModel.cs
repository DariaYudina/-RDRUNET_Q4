using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class IssueModel
    {
        public NewspaperModel Newspaper { get; set; }

        public int CountOfPublishing { get; set; }

        public DateTime DateOfPublishing { get; set; }
    }
}