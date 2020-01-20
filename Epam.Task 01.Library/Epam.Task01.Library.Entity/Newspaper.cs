using System;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper : AbstractLibraryItem
    {
        public Issue Issue { get; set; }

        public int CountOfPublishing { get; set; }

        public DateTime DateOfPublishing { get; set; }

        public Newspaper() { }

        public Newspaper(Issue issue, int yearOfPublishing, int countOfPublishing, DateTime dateOfPublishing, int pageCount, string commentary)
                        : base(issue.Title, pageCount, commentary)
        {

            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
        }
    }
}
