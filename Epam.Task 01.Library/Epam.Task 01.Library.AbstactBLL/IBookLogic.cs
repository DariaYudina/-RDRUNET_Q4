using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

// нарушение кодестайл

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IBookLogic
    {
        IEnumerable<Book> GetBookItems();
        bool AddBook(List<ValidationObject> validationResult, Book book);
        Book GetBookById(int id);
        IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany);
        bool CheckBookUniqueness(Book book);    // зачем?
    }
}
