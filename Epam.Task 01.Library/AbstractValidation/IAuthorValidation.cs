using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractValidation
{
    public interface IAuthorValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set; }
        IAuthorValidation CheckAuthorsFirstName(IWithAuthorProperty withAuthor);
        IAuthorValidation CheckAuthorsLastName(IWithAuthorProperty withAuthor);
    }
}
