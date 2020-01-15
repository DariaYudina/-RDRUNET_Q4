using System;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper : AbstractLibraryItem
    {
        public string City { get; set; }
        public string PublishingCompany { get; set; }
        public int CountOfPublishing { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string ISSN { get; set; }
        public Newspaper(string city, string publishingCompany, int yearOfPublishing, int countOfPublishing, DateTime dateOfPublishing, string issn,
                        string title, int pageCount, string commentary)
                        : base(title, pageCount, commentary)
        {
            City = city;
            PublishingCompany = publishingCompany;
            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            ISSN = issn;
        }
    }
}
