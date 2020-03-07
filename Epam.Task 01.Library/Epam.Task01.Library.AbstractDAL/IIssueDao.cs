using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IIssueDao
    {
        int AddIssue(Issue issue);

        IEnumerable<Issue> GetIssueItems();

    }
}
