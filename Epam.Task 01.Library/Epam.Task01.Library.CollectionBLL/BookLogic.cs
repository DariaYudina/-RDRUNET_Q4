using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using System.Collections.Generic;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using System.Linq;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionBLL
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookDao _bookDao;
        private readonly IBookValidation _bookValidation;

        public BookLogic(IBookDao bookDao, IBookValidation bookValidation)
        {
            _bookDao = bookDao;
            _bookValidation = bookValidation;
        }

        public bool AddBook(out ValidationObject validationObject, Book book)
        {
            validationObject = _bookValidation.ValidationObject;

            if (book == null)
            {
                validationObject.ValidationExceptions.Add(new ValidationException($"{nameof(book)} must be not null and not empty", nameof(book)));
                return false;
            }

            IBookValidation bookvalidation = _bookValidation.CheckByCommonValidation(book)
                                                                    .CheckBookCity(book)
                                                                    .CheckPublishingCompany(book)
                                                                    .CheckISBN(book)
                                                                    .CheckYearOfPublishing(book)
                                                                    .CheckAuthors(book);
            if (bookvalidation.ValidationObject.IsValid)
            {
                return _bookDao.AddBook(book) > 0;
            }

            return false;
        }

        public Book GetBookById(int id)
        {
            return _bookDao.GetBookById(id);
        }

        public IEnumerable<Book> GetBookItems()
        {
            return _bookDao.GetBookItems();
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _bookDao.GetBooksByAuthor(author);
        }

        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany)
        {
            return _bookDao.GetBooksByPublishingCompany(publishingCompany).GroupBy(item => item.PublishingCompany).ToList();
        }
    }
}
