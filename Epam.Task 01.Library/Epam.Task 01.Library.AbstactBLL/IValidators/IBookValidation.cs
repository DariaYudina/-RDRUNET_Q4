using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IBookValidation
    {
        List<ValidationObject> ValidationResult { get; set; }

        bool IsValid { get; set; }

        IBookValidation CheckBookCity(Book book);

        IBookValidation CheckPublishingCompany(Book book);

        IBookValidation CheckISBN(Book book);

        IBookValidation CheckYearOfPublishing(Book book);

        IBookValidation CheckByCommonValidation(Book book);

        IBookValidation CheckAuthors(Book book);

        bool CheckISBNLessThanBottomBorderISBNLength(string isbn);     // при чем тут граница леса?
    }
}
