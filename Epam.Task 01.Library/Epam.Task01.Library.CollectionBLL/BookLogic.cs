using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL
{
    public class BookLogic : IBookLogic
    {
        private IBookDao _bookDao;
        private ICommonValidation _commonValidation;
        private IBookValidation _bookValidation;
        private IAuthorValidation _authorValidation;
        public BookLogic(IBookDao bookDao, ICommonValidation commonValidation, IBookValidation bookValidation, IAuthorValidation authorValidation)
        {
            _bookDao = bookDao;
            _commonValidation = commonValidation;
            _bookValidation = bookValidation;
            _authorValidation = authorValidation;
        }
        public bool AddBook(List<ValidationException> validationResult, Book book)
        {
            _commonValidation.ValidationResult = validationResult;
            _bookValidation.ValidationResult = _commonValidation.ValidationResult;
            _authorValidation.ValidationResult = _bookValidation.ValidationResult;
            var commonvalidationObject = _commonValidation.CheckNullReferenceObject(book).CheckTitle(book).CheckPagesCount(book);
            var bookvalidationObject = _bookValidation.CheckBookCity(book).CheckPublishingCompany(book).CheckISBN(book).CheckYearOfPublishing(book);
            var authorsvalidationObject = _authorValidation.CheckAuthorsFirstName(book).CheckAuthorsLastName(book);
            if (!CheckBookUniqueness(book))
            {
                _authorValidation.ValidationResult.Add(new ValidationException("Book is not unique ", "Book"));
                return false;
            }
            if (commonvalidationObject.IsValid && bookvalidationObject.IsValid && authorsvalidationObject.IsValid )
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
