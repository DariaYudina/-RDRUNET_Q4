using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IBookDao
    {
        void AddBook(Book item);
        IEnumerable<Book> GetBookItems();
        Book GetBookById(int id);
        IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany);
        bool CheckBookUniqueness(Book book);
    }
}
