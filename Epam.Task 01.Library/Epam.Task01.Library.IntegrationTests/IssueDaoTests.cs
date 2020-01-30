using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.IntegrationTests
{
    [TestClass]
    public class IssueDaoTests
    {
        private Issue _defaultIssueItem;
        private IIssueDao _issueDao;

        [TestInitialize]
        public void Initialize()
        {
            _issueDao = new IssueDao();

            Issue defaultIssueItem = new Issue
            (
              title: "",
              city: "",
              publishingCompany: "",
              issn: ""
            );

            _defaultIssueItem = defaultIssueItem;
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
            MemoryStorage.DeleteIssueById(_defaultIssueItem.IssueId);
        }

        [TestMethod]
        public void GetIssueById_FoundExistingId_ReturnIssue()
        {
            // Arrange
            var expectedCount = _issueDao.GetIssueItems().Count() + 1;
            // Act
            _issueDao.AddIssue(_defaultIssueItem);
            var actualValidationResuilCount = _issueDao.GetIssueItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            MemoryStorage.DeleteIssueById(_defaultIssueItem.IssueId);
        }

        [TestMethod]
        public void GetIssueById_FoundNotExistingI_ReturnNull()
        {
            // Act

            Issue item = _issueDao.GetIssueItemById(_defaultIssueItem.IssueId);

            //Assert

            Assert.IsNull(item);
        }

        [TestMethod]
        public void GetIssueItems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange

            MemoryStorage.AddIssue(_defaultIssueItem);
            int expectedCount = 1;

            // Act

            var result = _issueDao.GetIssueItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, result);
            MemoryStorage.DeleteIssueById(_defaultIssueItem.IssueId);
        }

        [TestMethod]
        public void GetIssueItems_ToEmptyDao_ReturnEmptyCollection()
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
