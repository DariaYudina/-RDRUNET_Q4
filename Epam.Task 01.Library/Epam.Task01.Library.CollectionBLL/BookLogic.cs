using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionBLL
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookDao _bookDao;
        private  IBookValidation _bookValidation;

        public BookLogic(IBookDao bookDao, IBookValidation bookValidation)
        {
            _bookDao = bookDao;
            _bookValidation = bookValidation;
        }

        public bool AddBook(out ValidationObject validationObject, Book book)
        {
            try
            {
                validationObject = _bookValidation.ValidationObject;

                if (book == null)
                {
                    validationObject.ValidationExceptions.Add(new ValidationException($"{nameof(book)} must be not null and not empty", nameof(book)));
                    return false;
                }

                _bookValidation.CheckByCommonValidation(book)
                               .CheckBookCity(book)
                               .CheckPublishingCompany(book)
                               .CheckISBN(book)
                               .CheckYearOfPublishing(book);

                if (_bookValidation.ValidationObject.IsValid)
                {
                    return _bookDao.AddBook(book) > 0;
                }

                return false;
            }
            catch (Exception e)
            {

                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool EditBook(out ValidationObject validationObject, Book book)
        {
            try
            {
                validationObject = _bookValidation.ValidationObject;

                if (book == null)
                {
                    validationObject.ValidationExceptions.Add(new ValidationException($"{nameof(book)} must be not null and not empty", nameof(book)));
                    return false;
                }

                _bookValidation.CheckByCommonValidation(book)
                               .CheckBookCity(book)
                               .CheckPublishingCompany(book)
                               .CheckISBN(book)
                               .CheckYearOfPublishing(book);

                if (_bookValidation.ValidationObject.IsValid)
                {
                    return _bookDao.EditBook(book) >= 0;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public Book GetBookById(int id)
        {
            try
            {
                return _bookDao.GetBookById(id);
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            try
            {
                return _bookDao.GetBooks();
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            try
            {
                return _bookDao.GetBooksByAuthor(author);
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany)
        {
            try
            {
                return _bookDao.GetBooksByPublishingCompany(publishingCompany).GroupBy(item => item.PublishingCompany).ToList();
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}
