﻿using AbstractValidation;
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
            _bookValidation.ValidationResult = validationResult; // для чего это?

            if (book == null)
            {
                _bookValidation.ValidationResult.Add(new ValidationObject("Book must be not null and not empty", "Book"));
                return false;
            }

            if (book.Authors == null || book.Authors.Count == 0)    // кто автор у Библии, Большой Советской Энциклопедии?
            {
                _bookValidation.ValidationResult.Add(new ValidationObject("Authors must be not null and not empty", "Authors"));
                return false;
            }

            IBookValidation bookvalidationObject = _bookValidation.CheckByCommonValidation(book).CheckBookCity(book).CheckPublishingCompany(book).CheckISBN(book).CheckYearOfPublishing(book).CheckAuthors(book); //сломал глаза пока дочитал до конца
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

        public bool CheckBookUniqueness(Book book)      // кто у тебя еще использует этот метод снаружи класса?
        {
            return _bookDao.CheckBookUniqueness(book);
        }
    }
}
