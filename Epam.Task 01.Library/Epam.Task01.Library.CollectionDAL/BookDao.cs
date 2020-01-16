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
            //IEnumerable<AbstractLibraryItem> allLibrary = MemoryStorage.GetAllAbstractLibraryItems();
            //IEnumerable<Book> bookLibrary = allLibrary.OfType<Book>();
            //IEnumerable<AbstractLibraryItem> withauthor = allLibrary.OfType<AbstractLibraryItem>();
            //if (book.isbn != "" && book.isbn != null)
            //{
            //    foreach (Book item in bookLibrary)
            //    {
            //        if (item.isbn == book.isbn)
            //        {
            //            return false;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (AbstractLibraryItem item in allLibrary)
            //    {
            //        if (item.Title == book.Title || item.YearOfPublishing == book.YearOfPublishing)
            //        {
            //            return false;
            //        }
            //    }
            //    foreach (IWithAuthorProperty authors in withauthor)
            //    {
            //        bool res = true;
            //        for (int i = 0; i < withauthor.Count(); i++)
            //        {
            //            if (authors.Authors[i].FirstName == book.Authors[i].FirstName && authors.Authors[i].LastName == book.Authors[i].LastName)
            //            {
            //                res |= false;
            //            }
            //        }
            //        return res;
            //    }
            //}
            return true;
        }
    }
}
