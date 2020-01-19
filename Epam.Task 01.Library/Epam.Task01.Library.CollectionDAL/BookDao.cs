using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class BookDao : IBookDao
    {
        public void AddBook(Book item)
        {
            MemoryStorage.AddLibraryItem(item);
        }
        public IEnumerable<Book> GetBookItems()
        {
            return MemoryStorage.GetLibraryItemByType<Book>();
        }
        public Book GetBookById(int id)
        {
            return MemoryStorage.GetLibraryItemByType<Book>().FirstOrDefault(item => item.LibaryItemId == id);
        }
        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany)
        {
            return MemoryStorage.GetLibraryItemByType<Book>().Where(book => book.PublishingCompany.Contains(publishingCompany)).GroupBy(book => book.PublishingCompany);
        }
        public bool CheckBookUniqueness(Book book)
        {
            IEnumerable<Book> books = MemoryStorage.GetAllAbstractLibraryItems().OfType<Book>();
            IEnumerable<Patent> patents = MemoryStorage.GetAllAbstractLibraryItems().OfType<Patent>();
            var booksAndPatents = MemoryStorage.GetAllAbstractLibraryItems().Where(i => i is Book || i is Patent);
            if (book.isbn != "" && book.isbn != null)
            {
                foreach (Book item in books)
                {
                    if (item.isbn == book.isbn)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (AbstractLibraryItem item in MemoryStorage.GetAllAbstractLibraryItems())
                {
                    if (item.Title == book.Title || item.YearOfPublishing == book.YearOfPublishing)
                    {
                        return false;
                    }
                }
                bool res = true;
                foreach (var item in books)
                {
                    for (int i = 0; i < item.Authors.Count(); i++)
                    {
                        if (item.Authors[i].FirstName == book.Authors[i].FirstName && item.Authors[i].LastName == book.Authors[i].LastName)
                        {
                            res |= false;
                        }
                    }
                }
                foreach (var item in patents)
                {
                    for (int i = 0; i < item.Authors.Count(); i++)
                    {
                        if (item.Authors[i].FirstName == book.Authors[i].FirstName && item.Authors[i].LastName == book.Authors[i].LastName)
                        {
                            res |= false;
                        }
                    }
                }
                return res;
            }
            return true;
        }
    }
}
