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
    class NewspaperDaoTests
    {
        private Newspaper _defaultNewspaperItem;
        private INewspaperDao _newspaperDao;

        [TestInitialize]
        public void Initialize()
        {
            _newspaperDao = new NewspaperDao();

            Newspaper defaultNewspaperItem = new Newspaper
            (
                issue: new Issue("", "", "", ""),
                yearOfPublishing: 2000,
                countOfPublishing: 0,
                dateOfPublishing: DateTime.Now,
                pageCount: 1,
                commentary: ""
            );

            _defaultNewspaperItem = defaultNewspaperItem;
        }

        [TestMethod]
        public void AddNewspaper_AddingValidItem_Successfully()
        {
            // Arrange

            var expectedCount = _newspaperDao.GetNewspaperItems().Count() + 1;

            // Act
            _newspaperDao.AddNewspaper(_defaultNewspaperItem);
            var actualValidationResuilCount = _newspaperDao.GetNewspaperItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            MemoryStorage.DeleteLibraryItemById(_defaultNewspaperItem.LibaryItemId);
        }

        [TestMethod]
        public void GetIssueItems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange

            MemoryStorage.AddLibraryItem(_defaultNewspaperItem);
            int expectedCount = 1;

            // Act

            var result = _newspaperDao.GetNewspaperItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, result);
            MemoryStorage.DeleteLibraryItemById(_defaultNewspaperItem.LibaryItemId);
        }

        [TestMethod]
        public void GetIssueItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange

            int expectedCount = 0;

            // Act

            var result = _newspaperDao.GetNewspaperItems().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void CheckNewspaperUniqueness_UniquenessNewspaper_ReturnTrue()
        {
            // Arrange

            bool expectedCount = true;

            // Act

            var result = _newspaperDao.CheckNewspaperUniqueness(_defaultNewspaperItem);

            //Assert

            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public void CheckNewspaperUniqueness_NotUniquenessNewspaper_ReturnFalse()
        {
            // Arrange

            MemoryStorage.AddLibraryItem(_defaultNewspaperItem);
            bool expectedCount = false;

            // Act

            var result = _newspaperDao.CheckNewspaperUniqueness(_defaultNewspaperItem);

            //Assert

            Assert.AreEqual(expectedCount, result);
            MemoryStorage.DeleteLibraryItemById(_defaultNewspaperItem.LibaryItemId);
        }

    }
}
