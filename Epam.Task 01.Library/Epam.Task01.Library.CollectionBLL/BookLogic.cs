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
        private readonly ICommonValidation _commonValidation;
        private readonly IBookValidation _bookValidation;
        private readonly IAuthorValidation _authorValidation;
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
            ICommonValidation commonvalidationObject = _commonValidation.CheckNullReferenceObject(book).CheckTitle(book).CheckPagesCount(book);
            IBookValidation bookvalidationObject = _bookValidation.CheckBookCity(book).CheckPublishingCompany(book).CheckISBN(book).CheckYearOfPublishing(book);
            IAuthorValidation authorsvalidationObject = _authorValidation.CheckAuthorsFirstName(book).CheckAuthorsLastName(book);
            if (!CheckBookUniqueness(book))
            {
                _authorValidation.ValidationResult.Add(new ValidationException("Book is not unique ", "Book"));
                return false;
            }
            if (commonvalidationObject.IsValid && bookvalidationObject.IsValid && authorsvalidationObject.IsValid)
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
