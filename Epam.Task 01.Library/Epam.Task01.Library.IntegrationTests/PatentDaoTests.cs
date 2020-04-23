using System;
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
    public class PatentDaoTests
    {
        private Patent _defaultPatentItem;
        private IPatentDao _patentDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;
        private SqlConnectionConfig sqlConnectionConfig;

        [TestInitialize]
        public void Initialize()
        {
            sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"]
                .ConnectionString);
            _patentDao = new PatentDBDao(sqlConnectionConfig);
            _commonDao = new CommonDBDao(sqlConnectionConfig);

            Patent defaultPatentItem = new Patent
            (
                id: 3,
                authors: new List<Author>() { new Author("", "") },
                country: "",
                registrationNumber: "",
                applicationDate: DateTime.Now,
                publicationDate: DateTime.Now,
                title: "",
                pageCount: 1,
                commentary: ""
            );

            _defaultPatentItem = defaultPatentItem;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void AddPatent_AddingValidItem_Successfully()
        {
            // Arrange
            int expectedCount = _patentDao.GetPatents().Count() + 1;

            // Act
            _patentDao.AddPatent(_defaultPatentItem);
            int actualValidationResuilCount = _patentDao.GetPatents().Count();

            //Assert
            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            _commonDao.DeleteLibraryItemById(_defaultPatentItem.Id);
        }

        [TestMethod]
        public void GetPatentItems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange
            _patentDao.AddPatent(_defaultPatentItem);
            int expectedCount = 1;

            // Act
            int result = _patentDao.GetPatents().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
            _commonDao.DeleteLibraryItemById(_defaultPatentItem.Id);
        }

        [TestMethod]
        public void GetPatentItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange
            int expectedCount = 0;

            // Act
            int result = _patentDao.GetPatents().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
