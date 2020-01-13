using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractValidation
{
    public interface IBookValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set;}
        IBookValidation CheckBookCity(Book book);
        IBookValidation CheckPublishingCompany(Book book);
        IBookValidation CheckISBN(Book book);
        IBookValidation CheckYearOfPublishing(Book book);
    }
}
