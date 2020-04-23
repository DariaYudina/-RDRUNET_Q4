using System;

namespace Epam.Task01.Library.Entity
{
    public class Issue : AbstractLibraryItem
    {
        public Newspaper Newspaper { get; set; }

        public int? CountOfPublishing { get; set; }

        public DateTime DateOfPublishing { get; set; }

        public override int YearOfPublishing => DateOfPublishing.Year;

        public override string Title => Newspaper.Title;

        public Issue()
        { 
        }

        public Issue(Newspaper newspaper, int yearOfPublishing, int countOfPublishing,
                         DateTime dateOfPublishing, int pageCount, string commentary)
                : base(newspaper.Title, pageCount, commentary, yearOfPublishing)
        {
            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            Newspaper = newspaper;
        }

        public Issue(int id, Newspaper newspaper, int yearOfPublishing, int countOfPublishing,
                     DateTime dateOfPublishing, int pageCount, string commentary)
                        : base(id, newspaper.Title, pageCount, commentary, yearOfPublishing)
        {
            base.YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            Newspaper = newspaper;
        }
    }
}
