using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class IssueDao : IIssueDao
    {
        public void AddIssue(Issue issue)
        {
            MemoryStorage.AddIssue(issue);
        }

        public Issue GetIssueItemById(int id)
        {
            return MemoryStorage.GetIssueItemById(id);
        }

        public IEnumerable<Issue> GetIssueItems()
        {
            return MemoryStorage.GetAllIssues();
        }
    }
}
