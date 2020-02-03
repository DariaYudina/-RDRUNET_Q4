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
            return MemoryStorage.GetLibraryItemByType<Book>().FirstOrDefault(item => item.LibaryItemId == id);  // а может быть другая сущность с тем же Id?
        }

        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishing_Company)
        {
            return MemoryStorage.GetLibraryItemByType<Book>().Where(publishingCompany => publishingCompany.PublishingCompany.StartsWith(publishing_Company)).GroupBy(publishingCompany => publishingCompany.PublishingCompany).ToList();
        }

        public bool CheckBookUniqueness(Book book)
        {
            var books = MemoryStorage.GetLibraryItemByType<Book>();
            if (book.isbn != "" && book.isbn != null)
            {
                foreach (Book item in books)
                {
                    if (item.isbn == book.isbn) // а если нет, то ты начнешь проверять остальное?
                    {
                        return false;
                    }
                }
            }
            else
            {
                bool res = true;
                foreach (var item in books)
                {
                    for (int i = 0; i < item.Authors.Count(); i++)
                    {
                        // есть книга с авторами { Иван Петров, Петр Иванов} добавляю книгу с авторами { Петр Иванов, Иван Петров} что получу при проверке авторов?
                        if (item.Authors[i].FirstName == book.Authors[i].FirstName && item.Authors[i].LastName == book.Authors[i].LastName)
                        {
                            res &= false;   // имею книгу с автором {Иван Петров}, добавляю с авторами { Иван Петров, Петр Иванов}. Пройдет валидация на уникальность?
                        }
                    }
                    if (item.Title == book.Title && item.YearOfPublishing == book.YearOfPublishing && !res)
                    {
                        res &= false;
                    }
                }

                return res;
            }

            return true;
        }
    }
}
