using System;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper : AbstractLibraryItem
    {
        public string City { get; set; }
        public string PublishingCompany { get; set; }
        public int CountOfPublishing { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string issn { get; set; }
        public int YearOfPublishing { get; set; }
        public Newspaper() { }
        public Newspaper(string city, string publishingCompany, int yearOfPublishing, int countOfPublishing, DateTime dateOfPublishing, string issn,
                        string title, int pageCount, string commentary)
                        : base(title, pageCount, commentary)
        {
            City = city;
            PublishingCompany = publishingCompany;
            YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            this.issn = issn;
        }
    }
}
