using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

// нарушение кодестайл

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IBookLogic
    {
        IEnumerable<Book> GetBookItems();
        bool AddBook(out ValidationObject validationObject, Book book);
        Book GetBookById(int id);
        IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany);
        IEnumerable<Book> GetBooksByAuthor(Author author);
    }
}
