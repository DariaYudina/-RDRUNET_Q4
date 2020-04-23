using System.Collections.Generic;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IIssueDao
    {
        int AddIssue(Issue issue);

        IEnumerable<Issue> GetIssues();

        IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId, int currentId);

        int EditIssue(Issue issue);
    }
}
