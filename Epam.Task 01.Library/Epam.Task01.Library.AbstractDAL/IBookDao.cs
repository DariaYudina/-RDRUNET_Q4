using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IBookDao
    {
        int AddBook(Book item);  //неплохо было бы, чтобы при добавлении сущности возвращался Id

        IEnumerable<Book> GetBookItems();

        Book GetBookById(int id);

        IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany);

        IEnumerable<Book> GetBooksByPublishingCompany2(string publishingCompany);

        IEnumerable<Book> GetBooksByAuthor(Author author);

    }
}
