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
    public class BookDaoTests
    {
        private Book _defaultBookItem;
        private IBookDao _bookDao;
        private ICommonDao _commonDao;

        [TestInitialize]
        public void Initialize()
        {
            _bookDao = new BookDao();
            _commonDao = new CommonDao();

             Book defaultBookItem = new Book
            ( id: 1,
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
        }

        [TestMethod]
        public void AddBook_AddingValidItem_Successfully()
        {
            // Arrange

            var expectedCount = _bookDao.GetBookItems().Count() + 1;

            // Act
            _bookDao.AddBook(_defaultBookItem);
            var actualValidationResuilCount = _bookDao.GetBookItems().Count();

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

            var result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();
            foreach (var item in result)
            {
                foreach (var i in item)
                {
                    actualResult |= i.PublishingCompany == _defaultBookItem.PublishingCompany;
                }
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
            var result = _bookDao.GetBooksByPublishingCompany(foundCompany).ToList();

            //Assert

            Assert.AreEqual(0, result.Count);
        }

        
    }
}
