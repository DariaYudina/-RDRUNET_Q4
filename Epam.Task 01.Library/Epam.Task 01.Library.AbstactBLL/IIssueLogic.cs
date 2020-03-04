using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

// кодестайл 

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IIssueLogic
    {
        bool AddIssue(out ValidationObject validationObject, Issue issue);
        IEnumerable<Issue> GetIssueItems();
    }
}
