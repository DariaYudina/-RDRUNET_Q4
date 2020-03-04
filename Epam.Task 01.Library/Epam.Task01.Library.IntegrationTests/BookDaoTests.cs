using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;

namespace Epam.Task01.Library.IntegrationTests
{
    [TestClass]
    public class BookDaoTests
    {
        private Book _defaultBookItem;
        private IBookDao _bookDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            _bookDao = new BookDBDao();
            _commonDao = new CommonDBDao();

             Book defaultBookItem = new Book
            ( 
              authors: new List<Author>() {},
              city: "Test1",
              publishingCompany: "Test1",
              yearOfPublishing: 2020,
              isbn: "ISBN",
              title: "Test",
              pagesCount: 100,
              commentary: "Test1"
            );

            _defaultBookItem = defaultBookItem;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        [TestCleanup()]
        public void Cleanup()
        {
           // scope.Dispose();
        }
        [TestMethod]
        public void AddBook_AddingValidItem_Successfully()
        {
            // Arrange

            var expectedCount = _bookDao.GetBookItems().Count() + 1;

            // Act
            _bookDao.AddBook(_defaultBookItem);
            var actualValidationResuilCount = _bookDao.GetBookItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            _commonDao.DeleteLibraryItemById(_defaultBookItem.Id);
            
        }

        [TestMethod]
        public void GetBookById_FoundExistingId_ReturnBook()
        {
            // Arrange

            _bookDao.AddBook(_defaultBookItem);
            int foundId = _defaultBookItem.Id;
            // Act
            Book item = _bookDao.GetBookById(_defaultBookItem.Id);

            //Assert

            Assert.AreEqual(foundId, item.Id);
            _commonDao.DeleteLibraryItemById(_defaultBookItem.Id);
        }

        [TestMethod]
        public void GetBookById_FoundNotExistingI_ReturnNull()
        {
            // Act

            Book item = _bookDao.GetBookById(_defaultBookItem.Id);

            //Assert

            Assert.IsNull(item);
        }

        //[TestMethod]
        //public void GetBooksByPublishingCompany_FoundExistingPublishingompany_ReturnIGroupingItems()
        //{
        //    // Arrange

        //    _bookDao.AddBook(_defaultBookItem);
        //    string foundCompany = _defaultBookItem.PublishingCompany;
        //    bool actualResult = false;

        //    // Act

        //    var result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();
        //    foreach (var item in result)
        //    {
        //        foreach (var i in item)
        //        {
        //            actualResult |= i.PublishingCompany == _defaultBookItem.PublishingCompany;
        //        }
        //    }

        //    //Assert
        //    Assert.IsTrue(actualResult);
        //    _commonDao.DeleteLibraryItemById(_defaultBookItem.Id);
        //}

        [TestMethod]
        public void GetBooksByPublishingCompany_FoundExistingPublishingompany_ReturnEmptyIGroupingItems()
        {
            // Arrange
            string foundCompany = _defaultBookItem.PublishingCompany;
            // Act
            var result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();

            //Assert

            Assert.AreEqual(0, result.Count);
        }

        
    }
}
