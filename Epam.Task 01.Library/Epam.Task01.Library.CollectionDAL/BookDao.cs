using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class BookDao : IBookDao
    {
        public int AddBook(Book item)
        {
            if (BookIsUnique(item))
            {
                return MemoryStorage.AddLibraryItem(item);
            }

            return -1;
        }

        public IEnumerable<Book> GetBookItems()
        {
            return MemoryStorage.GetLibraryItemByType<Book>();
        }

        public Book GetBookById(int id)
        {
            return MemoryStorage.GetLibraryItemById(id) as Book;
        }

        public IEnumerable<Book> GetBooksByPublishingCompany(string publishingCompany)
        {
            return MemoryStorage.GetLibraryItemByType<Book>()
                                .Where(item => item.PublishingCompany.StartsWith(publishingCompany)).ToList();
        }

        private bool BookIsUnique(Book book)
        {
            var books = MemoryStorage.GetLibraryItemByType<Book>();
            if (string.IsNullOrEmpty(book.isbn) && book.isbn != null)
            {
                return !books.Any(i => i.isbn == book.isbn);
            }
            else
            {
                bool res = !books.Any( b => b.Title == book.Title && b.YearOfPublishing == book.YearOfPublishing);
                foreach (var item in book.Authors)
                {
                    res &= !(book.Authors.Any(a => a.Id == item.Id));
                }

                return res;
            }
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<Book>().Where(p => p.Authors.Any(item => item.Id == author.Id));
        }

    }
}
