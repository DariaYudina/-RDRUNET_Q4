using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Patent : AbstractLibraryItem, IWithAuthorProperty
    {
        public List<Author> Authors { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public Patent(List<Author> authors, string country, string registrationNumber, DateTime applicationDate, DateTime publicationDate,
                    int libaryItemId, string title, int pageCount, string commentary)
                    : base(libaryItemId, title, pageCount, commentary)
        {
            Authors = authors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
            PublicationDate = publicationDate;
            //todo base.YearOfPublishing = method();
        }
    }
}
