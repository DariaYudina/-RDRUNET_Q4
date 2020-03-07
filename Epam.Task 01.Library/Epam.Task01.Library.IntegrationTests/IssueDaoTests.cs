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
    class IssueDaoTests
    {
        private Issue _defaultIssueItem;
        private IIssueDao _issueDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            _issueDao = new IssueDBDao();
            _commonDao = new CommonDBDao();

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

            var expectedCount = _issueDao.GetIssueItems().Count() + 1;

            // Act
            _issueDao.AddIssue(_defaultIssueItem);
            var actualValidationResuilCount = _issueDao.GetIssueItems().Count();

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

            var result = _issueDao.GetIssueItems().Count();

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

            var result = _issueDao.GetIssueItems().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
