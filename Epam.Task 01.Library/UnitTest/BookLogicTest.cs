using System;
using System.Collections.Generic;
using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class BookLogicTest
    {
        [TestInitialize]
        public void Initialize()
        {
            MemoryStorage memoryStorage = new MemoryStorage();
        }

        //[TestMethod]
        //public void BooklogicAndBookDaoConsistency()
        //{
        //    BookDao dao = new BookDao();
        //    CommonValidation commonvalidation = new CommonValidation();
        //    BookValidation validation = new BookValidation(commonvalidation);
        //    List<ValidationObject> validationResult = new List<ValidationObject>();
        //    BookLogic logic = new BookLogic(dao, validation);
        //    Book book = new Book();
        //    {

        //    }
        //    Assert.IsTrue(logic.AddBook(validationResult, book));
        //}
        [TestMethod]
        public void CommonValidatorTest()
        {

        }
    }
}
