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
    public class CommonDaoTests
    {
        private Book _defaultBookItem;
        private ICommonDao _commonDao;
        private TransactionScope scope;
        private SqlConnectionConfig sqlConnectionConfig;

        [TestInitialize]
        public void Initialize()
        {
            sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"]
                .ConnectionString);
            _commonDao = new CommonDBDao(sqlConnectionConfig);

            Book defaultBookItem = new Book
           (id: 1,
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
            bool result = _commonDao.DeleteLibraryItemById(deletedItemId);

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
            int result = _commonDao.GetLibraryItems().Count();

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
            int result = _commonDao.GetLibraryItemsByTitle(foundedTitle).Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void GetTwoTypesByAuthor_FoundedNotExsistAuthor_ReturnItems()
        {
            // Arrange
            Author foundedAuthor = _defaultBookItem.Authors[0];
            int expectedCount = 0;

            // Act
            IEnumerable<AbstractLibraryItem> result = _commonDao.GetBookAndPatentByAuthorId(foundedAuthor.Id)
                .Where(i => (i is Patent && ((Patent)i).Authors.Any(a => a.Id == foundedAuthor.Id))
                         || (i is Book && ((Book)i).Authors.Any(a => a.Id == foundedAuthor.Id)));

            //Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void SortByYear_GetItemsEmptyCollection_ReturnEmptyCollection()
        {
            // Arrange
            int expectedCount = 0;

            // Act
            int result = _commonDao.SortByYear().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void SortByYearDesc_GetItemsEmptyCollection_ReturnEmptyCollection()
        {
            // Arrange
            int expectedCount = 0;

            // Act
            int result = _commonDao.SortByYearDesc().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
