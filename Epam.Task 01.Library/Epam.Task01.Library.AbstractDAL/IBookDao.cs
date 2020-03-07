using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IBookDao
    {
        int AddBook(Book item);

        IEnumerable<Book> GetBookItems();

        Book GetBookById(int id);

        IEnumerable<Book> GetBooksByAuthor(Author author);

        IEnumerable<Book> GetBooksByPublishingCompany(string publishingCompany);

    }
}
