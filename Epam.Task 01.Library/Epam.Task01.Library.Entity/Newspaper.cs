using System;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper : AbstractLibraryItem
    {
        public Issue Issue { get; set; }

        public int CountOfPublishing { get; set; }

        public DateTime DateOfPublishing { get; set; }

        public override int YearOfPublishing
        {
            get => DateOfPublishing.Year;
        }

        public Newspaper() { }

        public Newspaper(Issue issue, int yearOfPublishing, int countOfPublishing, 
                         DateTime dateOfPublishing, int pageCount, string commentary)
                : base(issue.Title, pageCount, commentary, yearOfPublishing)
        {

            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            Issue = issue;
        }

        public Newspaper(int id, Issue issue, int yearOfPublishing, int countOfPublishing, DateTime dateOfPublishing, int pageCount, string commentary)
                        : base(id, issue.Title, pageCount, commentary, yearOfPublishing)
        {

            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            Issue = issue;
        }

    }
}
