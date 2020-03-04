using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IIssueValidation
    {
        ValidationObject ValidationObject { get; set; }
        IIssueValidation CheckByCommonValidation(Issue issue);
        IIssueValidation CheckByNewspaperValidation(Issue issue);
        IIssueValidation CheckCountOfPublishing(Issue issue);
        IIssueValidation CheckDateOfPublishing(Issue issue);
        IIssueValidation CheckYearOfPublishing(Issue issue);
    }
}
