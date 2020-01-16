using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IAuthorValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set; }
        IAuthorValidation CheckAuthorsFirstName(AbstractLibraryItem withAuthor);
        IAuthorValidation CheckAuthorsLastName(AbstractLibraryItem withAuthor);
    }
}
