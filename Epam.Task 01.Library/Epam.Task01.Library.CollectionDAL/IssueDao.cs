using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class IssueDao : IIssueDao
    {
        public int AddIssue(Issue issue)
        {
            return MemoryStorage.AddLibraryItem(issue);
        }

        private bool CheckNewspaperUniqueness(Issue newspaper)
        {
            var newspapers = MemoryStorage.GetLibraryItemByType<Issue>();

            if ( newspaper.Newspaper.Issn != "" && newspaper.Newspaper.Issn != null)
            {
                foreach (Issue item in newspapers)
                {
                    if (item.Newspaper.Issn == newspaper.Newspaper.Issn && item.Title != newspaper.Title)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Issue item in newspapers)
                {
                    if (item.Title == newspaper.Title
                        && item.DateOfPublishing == newspaper.DateOfPublishing 
                        && item.Newspaper.PublishingCompany == newspaper.Newspaper.PublishingCompany)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IEnumerable<Issue> GetIssueItems()
        {
            return MemoryStorage.GetLibraryItemByType<Issue>();
        }
    }
}
