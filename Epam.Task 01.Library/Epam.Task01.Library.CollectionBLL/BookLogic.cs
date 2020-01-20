using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

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
        public bool AddBook(List<ValidationObject> validationResult, Book book)
        {
            _bookValidation.ValidationResult = validationResult;
            if(book == null)
            {
                _bookValidation.ValidationResult.Add(new ValidationObject("Object reference not set to an instance of an object", "Book"));
                return false;
            }
            IBookValidation bookvalidationObject = _bookValidation.CheckByCommonValidation(book).CheckBookCity(book).CheckPublishingCompany(book).CheckISBN(book).CheckYearOfPublishing(book).CheckAuthorsFirstName(book).CheckAuthorsLastName(book);
            if (!CheckBookUniqueness(book))
            {
                _bookValidation.ValidationResult.Add(new ValidationObject("Book is not unique ", "Book"));
                return false;
            }
            if ( bookvalidationObject.IsValid)
            {
                _bookDao.AddBook(book);
                return true;
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

        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany)
        {
            return _bookDao.GetBooksByPublishingCompany(publishingCompany);
        }

        public bool CheckBookUniqueness(Book book)
        {
            return _bookDao.CheckBookUniqueness(book);
        }
    }
}
