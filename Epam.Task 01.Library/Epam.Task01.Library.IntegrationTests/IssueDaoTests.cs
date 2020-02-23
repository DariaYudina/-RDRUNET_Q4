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
        private Newspaper _defaultIssueItem;
        private IIssueDao _issueDao;
        private ICommonDao _commonDao;

        [TestInitialize]
        public void Initialize()
        {
            _issueDao = new IssueDao();
            _commonDao = new CommonDao();

            Newspaper defaultIssueItem = new Newspaper
            (
              title: "",
              city: "",
              publishingCompany: "",
              issn: ""
            );

            _defaultIssueItem = defaultIssueItem;
        }

        //[TestMethod]
        //public void AddIssue_AddingValidItem_Successfully()
        //{
        //    Arrange

        //   var expectedCount = _issueDao.GetIssueItems().Count() + 1;

        //    Act
        //    _issueDao.AddIssue(_defaultIssueItem);
        //    var actualValidationResuilCount = _issueDao.GetIssueItems().Count();

        //    Assert

        //    Assert.AreEqual(expectedCount, actualValidationResuilCount);
        //    _commonDao.DeleteIssueItemById(_defaultIssueItem.IssueId);
        //}

        //[TestMethod]
        //public void GetIssueById_FoundExistingId_ReturnIssue()
        //{
        //    // Arrange
        //    var expectedCount = _issueDao.GetIssueItems().Count() + 1;
        //    // Act
        //    _issueDao.AddIssue(_defaultIssueItem);
        //    var actualValidationResuilCount = _issueDao.GetIssueItems().Count();

        //    //Assert

        //    Assert.AreEqual(expectedCount, actualValidationResuilCount);
        //    _commonDao.DeleteIssueItemById(_defaultIssueItem.IssueId);
        //}

        [TestMethod]
        public void GetIssueById_FoundNotExistingI_ReturnNull()
        {
            // Act

            Newspaper item = _issueDao.GetIssueItemById(_defaultIssueItem.Id);

            //Assert

            Assert.IsNull(item);
        }

        [TestMethod]
        //public void GetIssueItems_ToNotEmptyDao_ReturnItems()
        //{
        //    // Arrange

        //    _issueDao.AddIssue(_defaultIssueItem);
        //    int expectedCount = 1;

        //    // Act

        //    var result = _issueDao.GetIssueItems().Count();

        //    //Assert

        //    Assert.AreEqual(expectedCount, result);
        //    _commonDao.DeleteIssueItemById(_defaultIssueItem.IssueId);
        //}


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
