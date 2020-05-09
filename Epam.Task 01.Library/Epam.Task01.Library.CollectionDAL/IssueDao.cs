using System.Collections.Generic;
using System.Linq;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionDAL
{
    public class IssueDao : IIssueDao
    {
        public int AddIssue(Issue issue)
        {
            if (CheckIssueUniqueness(issue))
            {
                return MemoryStorage.AddLibraryItem(issue);
            }

            return -1;
        }

        public int EditIssue(Issue issue)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Issue> GetIssues()
        {
            return MemoryStorage.GetLibraryItemByType<Issue>();
        }

        public IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId, int currentId)
        {
            throw new System.NotImplementedException();
        }

        public int SoftDeleteIssue(int id)
        {
            throw new System.NotImplementedException();
        }

        private bool CheckIssueUniqueness(Issue issue)
        {
            IEnumerable<Issue> issues = MemoryStorage.GetLibraryItemByType<Issue>();

            if (issue.Newspaper.Issn != "" && issue.Newspaper.Issn != null)
            {
                if(issues.Any(i => i.Title == issue.Title && i.Newspaper.Issn == issue.Newspaper.Issn))
                {
                    return false;
                }
            }
            else
            {
                if (issues.Any(i => i.Title == issue.Title
                    && i.DateOfPublishing == issue.DateOfPublishing
                    && i.Newspaper.PublishingCompany == issue.Newspaper.PublishingCompany))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
