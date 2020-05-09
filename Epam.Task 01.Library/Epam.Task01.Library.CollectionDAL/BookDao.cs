using System.Collections.Generic;
using System.Linq;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;

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

        public IEnumerable<Book> GetBooks()
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

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<Book>().Where(p => p.Authors.Any(item => item.Id == author.Id));
        }

        private bool BookIsUnique(Book book)
        {
            IEnumerable<Book> books = MemoryStorage.GetLibraryItemByType<Book>();
            if (string.IsNullOrEmpty(book.Isbn) && book.Isbn != null)
            {
                return !books.Any(i => i.Isbn == book.Isbn);
            }
            else
            {
                bool res = !books.Any(b => b.Title == book.Title && b.YearOfPublishing == book.YearOfPublishing);
                foreach (Author item in book.Authors)
                {
                    res &= !(book.Authors.Any(a => a.Id == item.Id));
                }

                return res;
            }
        }

        public int EditBook(Book item)
        {
            throw new System.NotImplementedException();
        }

        public int SoftDeleteBook(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
