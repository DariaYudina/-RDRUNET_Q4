using AbstractValidation;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           return MemoryStorage.GetLibraryItemByType<Book>().Where(book => book.PublishingCompany == publishingCompany).GroupBy(book => book.PublishingCompany);
        }
        public bool CheckBookUniqueness(Book book)
        {
            var allLibrary = MemoryStorage.GetAllAbstractLibraryItems();
            var bookLibrary = allLibrary.OfType<Book>();
            var withauthor = allLibrary.OfType<IWithAuthorProperty>();
            if (book.ISBN != "" || book.ISBN!= null)
            {
                foreach (var item in bookLibrary)
                {
                    if (item.ISBN == book.ISBN)
                    {
                        return false;
                    }
                }
            }
            else 
            {
                foreach (var item in allLibrary)
                {
                    if(item.Title == book.Title || item.YearOfPublishing == book.YearOfPublishing)
                    {
                        return false;
                    }
                }
                foreach (var authors in withauthor)
                {
                    bool res = true;
                    for (int i = 0; i < withauthor.Count(); i++)
                    {
                        if(authors.Authors[i].FirstName == book.Authors[i].FirstName || authors.Authors[i].LastName == book.Authors[i].LastName)
                        {
                            res |= false;
                        }
                    }
                    return res;
                }
            }
            return true;
        }
    }
}
