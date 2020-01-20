using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Issue
    {
        public int IssueId { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string issn { get; set; }
    }
}
