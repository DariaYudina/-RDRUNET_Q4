using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class PatentModel
    {
        public List<AuthorModel> Authors { get; set; }

        public string Country { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}