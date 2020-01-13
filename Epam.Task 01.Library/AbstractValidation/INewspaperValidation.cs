using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractValidation
{
    public interface INewspaperValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set; }
        INewspaperValidation CheckNewspaperCity(Newspaper newspaper);
        INewspaperValidation CheckPublishingCompany(Newspaper newspaper);
        INewspaperValidation CheckYearOfPublishing(Newspaper newspaper);
        INewspaperValidation CheckISSN(Newspaper newspaper);
    }
}
