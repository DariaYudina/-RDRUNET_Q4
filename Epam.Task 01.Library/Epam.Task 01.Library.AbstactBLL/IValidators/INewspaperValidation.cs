using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface INewspaperValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        INewspaperValidation CheckByCommonValidation(Issue newspaper);
        INewspaperValidation CheckByIssueValidation(Issue newspaper);
        INewspaperValidation CheckCountOfPublishing(Issue newspaper);
        INewspaperValidation CheckDateOfPublishing(Issue newspaper);
        INewspaperValidation CheckYearOfPublishing(Issue newspaper);
    }
}
