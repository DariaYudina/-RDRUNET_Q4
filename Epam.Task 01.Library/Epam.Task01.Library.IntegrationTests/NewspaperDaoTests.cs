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
        private ICommonDao _commonDao;

        [TestInitialize]
        public void Initialize()
        {
            _newspaperDao = new NewspaperDao();
            _commonDao = new CommonDao();

            Newspaper defaultNewspaperItem = new Newspaper
            (
                id: 2,
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
            _commonDao.DeleteLibraryItemById(_defaultNewspaperItem.Id);
        }

        [TestMethod]
        public void GetNewspaperItems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange

            _newspaperDao.AddNewspaper(_defaultNewspaperItem);
            int expectedCount = 1;

            // Act

            var result = _newspaperDao.GetNewspaperItems().Count();

            //Assert

            Assert.AreEqual(expectedCount, result);
            _commonDao.DeleteLibraryItemById(_defaultNewspaperItem.Id);
        }

        [TestMethod]
        public void GetNewspaperItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange

            int expectedCount = 0;

            // Act

            var result = _newspaperDao.GetNewspaperItems().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
