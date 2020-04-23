using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epam.Task01.Library.IntegrationTests
{
    [TestClass]
    public class BookDaoTests
    {
        private Book _defaultBookItem;
        private IBookDao _bookDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;
        private SqlConnectionConfig sqlConnectionConfig;

        [TestInitialize]
        public void Initialize()
        {
            sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            _bookDao = new BookDBDao(sqlConnectionConfig);
            _commonDao = new CommonDBDao(sqlConnectionConfig);

            Book defaultBookItem = new Book
           (
             authors: new List<Author>() { },
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

        [TestCleanup]
        public void Cleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void AddBook_AddingValidItem_Successfully()
        {
            // Arrange
            int expectedCount = _bookDao.GetBooks().Count() + 1;

            // Act
            _bookDao.AddBook(_defaultBookItem);
            int actualValidationResuilCount = _bookDao.GetBooks().Count();

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

        [TestMethod]
        public void GetBooksByPublishingCompany_FoundExistingPublishingompany_ReturnIGroupingItems()
        {
            // Arrange
            _bookDao.AddBook(_defaultBookItem);
            string foundCompany = _defaultBookItem.PublishingCompany;
            bool actualResult = false;

            // Act
            List<Book> result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();
            foreach (Book item in result)
            {
                actualResult |= item.PublishingCompany == _defaultBookItem.PublishingCompany;
            }

            //Assert
            Assert.IsTrue(actualResult);
            _commonDao.DeleteLibraryItemById(_defaultBookItem.Id);
        }

        [TestMethod]
        public void GetBooksByPublishingCompany_FoundExistingPublishingompany_ReturnEmptyIGroupingItems()
        {
            // Arrange
            string foundCompany = _defaultBookItem.PublishingCompany;

            // Act
            List<Book> result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();

            //Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
