using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface INewspaperValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        INewspaperValidation CheckByCommonValidation(Newspaper newspaper);
        INewspaperValidation CheckByIssueValidation(Newspaper newspaper);
        INewspaperValidation CheckCountOfPublishing(Newspaper newspaper);
        INewspaperValidation CheckDateOfPublishing(Newspaper newspaper);
        INewspaperValidation CheckYearOfPublishing(Newspaper newspaper);
    }
}
