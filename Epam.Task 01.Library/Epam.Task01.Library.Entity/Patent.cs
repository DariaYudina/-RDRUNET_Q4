using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Patent : AbstractLibraryItem
    {
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public Patent(string country, string registrationNumber, DateTime applicationDate, DateTime publicationDate,
                    int libaryItemId, string title, int pageCount, string commentary)
                    : base(libaryItemId, title, pageCount, commentary)
        {
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
            PublicationDate = publicationDate;
        }
    }
}
