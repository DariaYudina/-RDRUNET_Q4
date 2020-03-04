using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IBookValidation
    {
        ValidationObject ValidationObject { get; set; }

        IBookValidation CheckBookCity(Book book);

        IBookValidation CheckPublishingCompany(Book book);

        IBookValidation CheckISBN(Book book);

        IBookValidation CheckYearOfPublishing(Book book);

        IBookValidation CheckByCommonValidation(Book book);

        IBookValidation CheckAuthors(Book book);
    }
}
