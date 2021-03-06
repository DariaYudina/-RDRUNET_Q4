using System;
using System.Collections.Generic;

namespace Epam.Task01.Library.Entity
{
    public class Patent : AbstractLibraryItem
    {
        public List<Author> Authors { get; set; }

        public string Country { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public DateTime PublicationDate { get; set; }

        public override int YearOfPublishing { get => PublicationDate.Year;  }

        public Patent()
        { 
        }

        public Patent(List<Author> authors, 
            string country, 
            string registrationNumber, 
            DateTime applicationDate, 
            DateTime publicationDate,
            string title, int pageCount, 
            string commentary)
            : base(title, 
                  pageCount,
                  commentary)
        {
            Authors = authors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
            PublicationDate = publicationDate;
        }

        public Patent(int id, List<Author> authors, string country, string registrationNumber, DateTime applicationDate, DateTime publicationDate,
                    string title, int pageCount, string commentary)
                    : base(id, title, pageCount, commentary)
        {
            Authors = authors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
            PublicationDate = publicationDate;
        }
    }
}
