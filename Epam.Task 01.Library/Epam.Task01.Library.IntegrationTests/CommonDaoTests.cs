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

namespace Epam.Task01.Library.IntegrationTests

{
    [TestClass]
    public class CommonDaoTests 
    {
        private Book _defaultBookItem;
        private ICommonDao _commonDao;
        private TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            _commonDao = new CommonDBDao();

            Book defaultBookItem = new Book
           ( id: 1,
             authors: new List<Author>() { new Author("", "") },
             city: "",
             publishingCompany: "",
             yearOfPublishing: 0,
             isbn: "",
             title: "",
             pagesCount: 0,
             commentary: ""
           );

            _defaultBookItem = defaultBookItem;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void DeleteLibraryItemById_ByNotExsistId_ReturnFalse()
        {
            // Arrange

            int deletedItemId = _defaultBookItem.Id;

            // Act
            
            var result = _commonDao.DeleteLibraryItemById(deletedItemId);

            //Assert
            
            Assert.IsFalse(result);
            _commonDao.DeleteLibraryItemById(_defaultBookItem.Id);
        }

        [TestMethod]
        public void GetAllAbstractLibraryItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange

            int expectedCount = 0;

            // Act

            var result = _commonDao.GetAllAbstractLibraryItems().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void GetLibraryItemsByTitle_NotFoundedTitle_ReturnEmptyCollection()
        {
            // Arrange

            string foundedTitle = _defaultBookItem.Title;
            int expectedCount = 0;

            // Act

            var result = _commonDao.GetLibraryItemsByTitle(foundedTitle).Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        //[TestMethod]
        //public void GetTypeByAuthor_FoundedNotExsistAuthor_ReturnEmtyCollection()
        //{
        //    // Arrange

        //    var foundedAuthor = _defaultBookItem.Authors[0];
        //    int expectedCount = 0;

        //    // Act

        //    var result = _commonDao.GetTypeByAuthor<Book>().Where(item => item.Authors.Contains(foundedAuthor)).ToList();

        //    //Assert
        //    Assert.AreEqual(expectedCount, result.Count());
        //}


        //[TestMethod]
        //public void GetTwoTypesByAuthor_FoundedNotExsistAuthor_ReturnItems()
        //{
        //    // Arrange
        //    var foundedAuthor = _defaultBookItem.Authors[0];
        //    int expectedCount = 0;

        //    // Act

        //    var result = _commonDao.GetTwoTypesByAuthor<Book, Patent>().Where(i => (i is Patent && ((Patent)i).Authors.Contains(foundedAuthor))
        //                 || (i is Book && ((Book)i).Authors.Contains(foundedAuthor)));


        //    //Assert
        //    Assert.AreEqual(expectedCount, result.Count());
        //}

        [TestMethod]
        public void SortByYear_GetItemsEmptyCollection_ReturnEmptyCollection()
        {
            // Arrange

            int expectedCount = 0;

            // Act

            var result = _commonDao.SortByYear().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void SortByYearDesc_GetItemsEmptyCollection_ReturnEmptyCollection()
        {
            // Arrange

            int expectedCount = 0;

            // Act

            var result = _commonDao.SortByYearDesc().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

    }
}
