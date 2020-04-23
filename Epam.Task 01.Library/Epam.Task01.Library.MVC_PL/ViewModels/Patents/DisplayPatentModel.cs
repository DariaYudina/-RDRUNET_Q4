using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class DisplayPatentModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PagesCount { get; set; }

        public string Commentary { get; set; }

        public int YearOfPublishing { get; set; }

        public List<DisplayAuthorModel> Authors { get; set; }

        public string Country { get; set; }

        public string RegistrationNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "Date not defined" )]
        public DateTime? ApplicationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime PublicationDate { get; set; }
    }
}