using AbstractValidation;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class CommonLogicTests
    {
        private AbstractLibraryItem _defaultAbstractLibraryItemItem;

        private CommonLogic _commonLogic;
        private Mock<ICommonDao> _commonDaoMock;
        private Mock<ICommonValidation> _commonValidationMock;
        private Author author;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidationMock = new Mock<ICommonValidation>();
            _commonDaoMock = new Mock<ICommonDao>();
            _commonLogic = new CommonLogic(_commonDaoMock.Object, _commonValidationMock.Object);
            author = new Author("Name", "LastName");
            Book defaultAbstractItem = new Book
            (authors: new List<Author>() { author },
              city: "",
              publishingCompany: "",
              yearOfPublishing: 0,
              isbn: "",
              title: "",
              pagesCount: 0,
              commentary: ""
            );

            _defaultAbstractLibraryItemItem = defaultAbstractItem;
        }

        [TestMethod]
        public void GetAllAbstractLibraryItems__GetAnyItemsInNotEmptyDao_ReturnListWithItems()
        {
            // Arrange

            List<AbstractLibraryItem> items = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem, _defaultAbstractLibraryItemItem, _defaultAbstractLibraryItemItem };
            _commonDaoMock.Setup(b => b.GetAllAbstractLibraryItems()).Returns(items);

            // Act

            List<AbstractLibraryItem> actualItems = _commonLogic.GetAllLibraryItems().ToList();

            //Assert

            CollectionAssert.AreEqual(items, actualItems);
        }

        [TestMethod]
        public void GetAllAbstractLibraryItems__GetAnyItemsInDao_ReturnEmptyListItems()
        {
            // Arrange

            List<AbstractLibraryItem> items = new List<AbstractLibraryItem>();
            _commonDaoMock.Setup(b => b.GetAllAbstractLibraryItems()).Returns(items);

            // Act

            List<AbstractLibraryItem> actualItems = _commonLogic.GetAllLibraryItems().ToList();

            //Assert

            CollectionAssert.AreEqual(items, actualItems);
        }

        [TestMethod]
        public void GetLibraryItemsByTitle__FoundedTitle_ReturnIEnumerableAbstractItem()
        {
            // Arrange
            List<AbstractLibraryItem> founded = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem };
            _commonDaoMock.Setup(b => b.GetLibraryItemsByTitle(It.IsAny<string>())).Returns(founded);

            // Act

            var result = _commonLogic.GetLibraryItemsByTitle("title").ToList();

            //Assert

            CollectionAssert.AreEqual(founded, result);
        }

        [TestMethod]
        public void GetLibraryItemsByTitle__NotFoundedTitle_ReturnEmptyIEnumerableAbstractItem()
        {
            // Arrange
            List<AbstractLibraryItem> founded = new List<AbstractLibraryItem>() {};
            _commonDaoMock.Setup(b => b.GetLibraryItemsByTitle(It.IsAny<string>())).Returns(founded);

            // Act

            var result = _commonLogic.GetLibraryItemsByTitle("title").ToList();

            //Assert

            CollectionAssert.AreEqual(founded, result);
        }
        
        [TestMethod]
        public void DeleteLibraryItemById__FoundedItemmById_ReturnTrue()
        {
            // Arrange
            int id = 1;
            _commonDaoMock.Setup(b => b.DeleteLibraryItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(true);

            // Act

            var result = _commonLogic.DeleteLibraryItemById(id);

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteLibraryItemById__NotFoundedItemmById_ReturnFalse()
        {
            // Arrange
            int id = 12;
            _commonDaoMock.Setup(b => b.DeleteLibraryItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(false);

            // Act

            var result = _commonLogic.DeleteLibraryItemById(id);

            //Assert

            Assert.IsFalse(result);
        }

        //[TestMethod]
        //public void GetLibraryItemsByYearOfPublishing__FoundedInDaoByYearOfPublishing_ReturnIGroupingItems()
        //{
        //    // Arrange

        //    List<IGrouping<int, AbstractLibraryItem>> items = new List<IGrouping<int, AbstractLibraryItem>>();

        //    _commonDaoMock.Setup(b => b.GetLibraryItemsByYearOfPublishing()).Returns(items);

        //    // Act

        //    var result = _commonLogic.GetLibraryItemsByYearOfPublishing();

        //    //Assert

        //    Assert.AreEqual(items, result);
        //}

        //[TestMethod]
        //public void GetBooksAndPatentsByAuthor__AuthorsFounded_ReturnFoundedItems()
        //{
        //    // Arrange

        //    List<AbstractLibraryItem> founded = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem};
        //    int authorId = 1;

        //    _commonDaoMock.Setup(b => b.GetBookAndPatentByAuthorId(authorId)).Returns(founded);

        //    // Act

        //    var result = _commonLogic.GetBooksAndPatentsByAuthor(author);

        //    //Assert

        //    Assert.AreEqual(founded.Count, result.Count());
        //}

        [TestMethod]
        public void GetBooksAndPatentsByAuthor__AuthorsNotFounded_ReturnFoundedItems1()
        {
            // Arrange

            List<AbstractLibraryItem> founded = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem };
            int authorId = 1;

            _commonDaoMock.Setup(b => b.GetBookAndPatentByAuthorId(authorId)).Returns(founded);

            // Act

            var result = _commonLogic.GetBooksAndPatentsByAuthor(new Author("", ""));

            //Assert

            Assert.AreNotEqual(founded.Count, result.Count());
        }

        //[TestMethod]
        //public void GetBooksByAuthor__AuthorsFounded_ReturnFoundedItems()
        //{
        //    // Arrange

        //    List<Book> founded = new List<Book>() { (Book)_defaultAbstractLibraryItemItem };
        //    _commonDaoMock.Setup(b => b.GetTypeByAuthor<Book>()).Returns(founded);

        //    // Act

        //    var result = _commonLogic.GetBooksByAuthor(author);

        //    //Assert

        //    Assert.AreEqual(founded.Count, result.Count());
        //}

        //[TestMethod]
        //public void GetBooksByAuthor__AuthorsNotFounded_ReturnFoundedItems1()
        //{
        //    // Arrange

        //    List<Book> founded = new List<Book>() { (Book)_defaultAbstractLibraryItemItem };
        //    _commonDaoMock.Setup(b => b.GetTypeByAuthor<Book>()).Returns(founded);

        //    // Act

        //    var result = _commonLogic.GetBooksByAuthor(new Author("", ""));

        //    //Assert

        //    Assert.AreNotEqual(founded.Count, result.Count());
        //}

        //[TestMethod]
        //public void GetPatentsByAuthor__AuthorsFounded_ReturnFoundedItems()
        //{
        //    // Arrange

        //    List<Patent> founded = new List<Patent>() { new Patent(new List<Author>() { author }, "", "", DateTime.Now, DateTime.Now, "", 1, "") };
        //    _commonDaoMock.Setup(b => b.GetTypeByAuthor<Patent>()).Returns(founded);

        //    // Act

        //    var result = _commonLogic.GetPatentsByAuthor(author);

        //    //Assert

        //    Assert.AreEqual(founded.Count, result.Count());
        //}

        //[TestMethod]
        //public void GetPatentsByAuthor__AuthorsNotFounded_ReturnFoundedItems()
        //{
        //    // Arrange

        //    List<Patent> founded = new List<Patent>() { new Patent(new List<Author>(), "", "", DateTime.Now, DateTime.Now,"", 1, "") };
        //    _commonDaoMock.Setup(b => b.GetTypeByAuthor<Patent>()).Returns(founded);

        //    // Act

        //    var result = _commonLogic.GetPatentsByAuthor(author);

        //    //Assert

        //    Assert.AreNotEqual(founded.Count, result.Count());
        //}

        [TestMethod]
        public void SortByYear__AuthorsNotFounded_ReturnSortedItems()
        {
            // Arrange

            List<AbstractLibraryItem> forSort = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem, _defaultAbstractLibraryItemItem };
            _commonDaoMock.Setup(b => b.SortByYear()).Returns(forSort);

            // Act

            var result = _commonLogic.SortByYear().ToList();

            //Assert

            Assert.AreEqual(forSort.Count, result.Count());
        }

        [TestMethod]
        public void SortByYear__AuthorsNotFounded_ReturnSortedDescItems()
        {
            // Arrange

            List<AbstractLibraryItem> forSort = new List<AbstractLibraryItem>() { _defaultAbstractLibraryItemItem, _defaultAbstractLibraryItemItem };
            _commonDaoMock.Setup(b => b.SortByYearDesc()).Returns(forSort);

            // Act

            var result = _commonLogic.SortByYearDesc().ToList();

            //Assert

            Assert.AreEqual(forSort.Count, result.Count());
        }
    }
}
