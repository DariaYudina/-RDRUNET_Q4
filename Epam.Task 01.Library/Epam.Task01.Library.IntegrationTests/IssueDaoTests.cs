using System;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epam.Task01.Library.IntegrationTests
{
    internal class IssueDaoTests
    {
        private Issue _defaultIssueItem;
        private IIssueDao _issueDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;
        private SqlConnectionConfig sqlConnectionConfig;

        [TestInitialize]
        public void Initialize()
        {
            sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"]
                .ConnectionString);
            _issueDao = new IssueDBDao(sqlConnectionConfig);
            _commonDao = new CommonDBDao(sqlConnectionConfig);

            Issue defaultNewspaperItem = new Issue
            (
                id: 2,
                newspaper: new Newspaper("", "", "", ""),
                yearOfPublishing: 2000,
                countOfPublishing: 0,
                dateOfPublishing: DateTime.Now,
                pageCount: 1,
                commentary: ""
            );

            _defaultIssueItem = defaultNewspaperItem;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void AddIssue_AddingValidItem_Successfully()
        {
            // Arrange
            int expectedCount = _issueDao.GetIssues().Count() + 1;

            // Act
            _issueDao.AddIssue(_defaultIssueItem);
            int actualValidationResuilCount = _issueDao.GetIssues().Count();

            //Assert
            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            _commonDao.DeleteLibraryItemById(_defaultIssueItem.Id);
        }

        [TestMethod]
        public void GetIssuetems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange
            _issueDao.AddIssue(_defaultIssueItem);
            int expectedCount = 1;

            // Act
            int result = _issueDao.GetIssues().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
            _commonDao.DeleteLibraryItemById(_defaultIssueItem.Id);
        }

        [TestMethod]
        public void GetNewspaperItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange
            int expectedCount = 0;

            // Act
            int result = _issueDao.GetIssues().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
