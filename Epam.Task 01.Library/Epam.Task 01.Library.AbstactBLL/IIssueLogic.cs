using System.Collections.Generic;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IIssueLogic
    {
        bool AddIssue(out ValidationObject validationObject, Issue issue);

        IEnumerable<Issue> GetIssues();

        IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId, int currentId);
        bool EditIssue(out ValidationObject validationObject, Issue issue);
    }
}
