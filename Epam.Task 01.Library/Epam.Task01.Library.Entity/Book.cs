using System.Collections.Generic;

namespace Epam.Task01.Library.Entity
{
    public class Book : AbstractLibraryItem
    {
        public List<Author> Authors { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string Isbn { get; set; }


        public Book() 
        {
        }

        public Book(List<Author> authors, string city, string publishingCompany, int yearOfPublishing, string isbn,
            string title, int pagesCount, string commentary)
            : base(title, pagesCount, commentary, yearOfPublishing)
        {
            Authors = authors;
            City = city;
            PublishingCompany = publishingCompany;
            base.YearOfPublishing = yearOfPublishing;
            this.Isbn = isbn;
        }

        public Book(int id, List<Author> authors, string city, string publishingCompany, int yearOfPublishing, string isbn,
                    string title, int pagesCount, string commentary)
                    : base(id, title, pagesCount, commentary, yearOfPublishing)
        {
            Authors = authors;
            City = city;
            PublishingCompany = publishingCompany;
            base.YearOfPublishing = yearOfPublishing;
            this.Isbn = isbn;
        }

        public override string ToString()
        {
            return string.Join(", ", Authors);
        }
    }
}
