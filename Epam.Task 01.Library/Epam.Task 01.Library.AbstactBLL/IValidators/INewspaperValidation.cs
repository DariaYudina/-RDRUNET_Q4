using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface INewspaperValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        INewspaperValidation CheckNewspaperCity(Newspaper newspaper);
        INewspaperValidation CheckPublishingCompany(Newspaper newspaper);
        INewspaperValidation CheckYearOfPublishing(Newspaper newspaper);
        INewspaperValidation CheckISSN(Newspaper newspaper);
        INewspaperValidation CheckByCommonValidation(Newspaper newspaper)
    }
}
