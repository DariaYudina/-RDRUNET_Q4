using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

// кодестайл 

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IIssueLogic
    {
        bool AddIssue(List<ValidationObject> validationResult, Issue issue);
        IEnumerable<Issue> GetIssueItems();
    }
}
