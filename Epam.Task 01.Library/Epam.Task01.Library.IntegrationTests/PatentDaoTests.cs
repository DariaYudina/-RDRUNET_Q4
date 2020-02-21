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
    public class PatentDaoTests
    {
        private Patent _defaultPatentItem;
        private IPatentDao _patentDao;
        private ICommonDao _commonDao;

        [TestInitialize]
        public void Initialize()
        {
            _patentDao = new PatentDao();
            _commonDao = new CommonDao();

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
        }

        [TestMethod]
        public void AddPatent_AddingValidItem_Successfully()
        {
            // Arrange

            var expectedCount = _patentDao.GetPatentItems().Count() + 1;

            // Act
            _patentDao.AddPatent(_defaultPatentItem);
            var actualValidationResuilCount = _patentDao.GetPatentItems().Count();

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

            var result = _patentDao.GetPatentItems().Count();

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

            var result = _patentDao.GetPatentItems().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
