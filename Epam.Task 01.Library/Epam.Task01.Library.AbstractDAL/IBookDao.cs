using System.Collections.Generic;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IBookDao
    {
        int AddBook(Book item);

        IEnumerable<Book> GetBooks();

        Book GetBookById(int id);

        IEnumerable<Book> GetBooksByAuthor(Author author);

        IEnumerable<Book> GetBooksByPublishingCompany(string publishingCompany);

        int EditBook(Book item);

        int SoftDeleteBook(int id);
    }
}
