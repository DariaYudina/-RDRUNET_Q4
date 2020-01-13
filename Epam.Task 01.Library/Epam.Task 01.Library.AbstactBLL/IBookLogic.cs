﻿using AbstractValidation;
using CollectionValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IBookLogic 
    {
        IEnumerable<Book> GetBookItems();
        bool AddBook(List<ValidationException> validationResult, Book book);
        Book GetBookById(int id);
        IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany);
        bool CheckBookUniqueness(Book book);
    }
}
