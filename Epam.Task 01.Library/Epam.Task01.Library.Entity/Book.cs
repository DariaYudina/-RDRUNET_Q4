﻿using System.Collections.Generic;

namespace Epam.Task01.Library.Entity
{
    public class Book : AbstractLibraryItem
    {
        public List<Author> Authors { get; set; }

        public string City { get; set; }

        public string PublishingCompany { get; set; }

        public string isbn { get; set; }

        public Book(List<Author> authors, string city, string publishingCompany, int yearOfPublishing, string isbn,
                    string title, int pagesCount, string commentary)
                    : base(title, pagesCount, commentary)
        {
            Authors = authors;
            City = city;
            PublishingCompany = publishingCompany;
            base.YearOfPublishing = yearOfPublishing;
            this.isbn = isbn;
        }
    }
}
