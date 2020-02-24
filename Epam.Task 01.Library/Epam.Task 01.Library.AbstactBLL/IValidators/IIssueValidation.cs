using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IIssueValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        IIssueValidation CheckByCommonValidation(Issue issue);
        IIssueValidation CheckByNewspaperValidation(Issue issue);
        IIssueValidation CheckCountOfPublishing(Issue issue);
        IIssueValidation CheckDateOfPublishing(Issue issue);
        IIssueValidation CheckYearOfPublishing(Issue issue);
    }
}
