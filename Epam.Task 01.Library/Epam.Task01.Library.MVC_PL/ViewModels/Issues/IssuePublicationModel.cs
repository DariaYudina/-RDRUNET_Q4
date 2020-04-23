using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class IssuePublicationModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountOfPublishing { get; set; }
    }
}