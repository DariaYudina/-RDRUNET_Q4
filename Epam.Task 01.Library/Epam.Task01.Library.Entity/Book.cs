using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Book : AbstractLibraryItem, IWithAuthorProperty
    {
        public List<Author> Authors { get; set; }
        public string City { get; set; }
        public string PublishingCompany { get; set; }
        public string ISBN { get; set; }
        public int YearOfPublishing { get; set; }
       
        public Book(List<Author> authors, string city, string publishingCompany, int yearOfPublishing, string isbn,
                    int libaryItemId, string title, int pageCount, string commentary) 
                    : base(libaryItemId, title, pageCount, commentary)
        {
            Authors = authors;
            City = city;
            PublishingCompany = publishingCompany;
            YearOfPublishing = yearOfPublishing;
            ISBN = isbn;
        }
    }
}
