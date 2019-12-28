using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Book : AbstractLibraryItem
    {
        public string City { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime YearOfPublishing { get; set; }
        public string ISBN { get; set; }
        public Book(string city, string publishingCompany, DateTime yearOfPublishing, string isbn,
                    int libaryItemId, string title, int pageCount, string commentary) 
                    : base(libaryItemId, title, pageCount, commentary)
        {
            City = city;
            PublishingCompany = publishingCompany;
            YearOfPublishing = yearOfPublishing;
            ISBN = isbn;
        }
    }
}
