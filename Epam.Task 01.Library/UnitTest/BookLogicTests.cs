using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
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
    public class BookLogicTests
    {
        private Book _defaultBookItem;

        private BookLogic _bookLogic;
        private Mock<IBookDao> _bookDaoMock;
        private Mock<IBookValidation> _bookValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _bookValidationMock = new Mock<IBookValidation>();
            _bookDaoMock = new Mock<IBookDao>();
            _bookLogic = new BookLogic(_bookDaoMock.Object, _bookValidationMock.Object);

            Book defaultBookItem = new Book
            (authors: new List<Author>() { new Author("", "") },
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
        public void AddBook_AddingValidBook_ReturnTrue()  
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.AddBook(It.IsAny<Book>()))
                .Callback<Book>(book => books.Add(book));
            _bookDaoMock.Setup(b => b.GetBooks()).Returns(books);

            _bookValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(true);
            _bookValidationMock.Setup(s => s.ValidationObject.ValidationExceptions).Returns(validationObjects.ValidationExceptions);
            _bookValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckBookCity(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckISBN(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Book>())).Returns(_bookValidationMock.Object);

            // Act

            var res = _bookLogic.AddBook(out validationObjects, _defaultBookItem);
            var actualValidationResuilCount = validationObjects.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(res);
            Assert.IsTrue(books.Contains(_defaultBookItem));
        }

        [TestMethod]
        public void AddBook_AddingNotValidBook_ReturnFalse()
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.GetBooks()).Returns(books);

            _bookValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(false);
            _bookValidationMock.Setup(s => s.ValidationObject.ValidationExceptions).Returns(validationObjects.ValidationExceptions);
            _bookValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckBookCity(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckISBN(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Book>())).Returns(_bookValidationMock.Object);

            // Act

            var res = _bookLogic.AddBook(out validationObjects, _defaultBookItem);

            //Assert
            Assert.IsFalse(res);
            Assert.IsFalse(books.Contains(_defaultBookItem));
        }

        [TestMethod]
        public void GetBookById_IdFoundedBookInCollection_ReturnBook()
        {
            //Arrange
            _bookDaoMock.Setup(b => b.GetBookById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultBookItem);

            //Act

            var resultBook = _bookLogic.GetBookById(5);

            //Assert

            Assert.IsNotNull(resultBook);
        }

        [TestMethod]
        public void GetBookById_IdNotFoundedBookInCollection_ReturnNull()
        {
            //Arrange
            _bookDaoMock.Setup(b => b.GetBookById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultBookItem);

            //Act

            var resultBook = _bookLogic.GetBookById(11);

            //Assert

            Assert.IsNull(resultBook);
        }

        [TestMethod]
        public void GetBookItems_GetAnyBooksInNotEmptyDao_ReturnListWithBooks()
        {
            //Arrange

            List<Book> books = new List<Book>() { _defaultBookItem, _defaultBookItem, _defaultBookItem };
            _bookDaoMock.Setup(b => b.GetBooks()).Returns(books);

            //Act

            List<Book> actualBooks = _bookLogic.GetBooks().ToList();

            //Assert

            CollectionAssert.AreEqual(books, actualBooks);
        }

        [TestMethod]
        public void GetBookItems_GetAnyBookInEmptyDao_ReturnEmptyList()
        {
            //Arrange

            List<Book> books = new List<Book>();
            _bookDaoMock.Setup(b => b.GetBooks()).Returns(books);

            //Act

            List<Book> actualBooks = _bookLogic.GetBooks().ToList();

            //Assert

            CollectionAssert.AreEqual(books, actualBooks);
        }

        [TestMethod]
        public void GetBooksByPublishingCompany_FoundedInDaoByPublishingCompany_ReturnIGroupingBooks()
        {
            // Arrange

            IEnumerable<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.GetBooksByPublishingCompany(It.IsAny<string>())).Returns(books);

            // Act

            IEnumerable<IGrouping<string, Book>> result = _bookLogic.GetBooksByPublishingCompany("foundedCompany");

            //Assert

            Assert.AreEqual(books, result);
        }

        [TestMethod]
        public void GetBooksByPublishingCompany_NotFoundedInDaoByPublishingCompany_ReturnEmptyIgroupingBooks()
        {
            // Arrange

            IEnumerable<Book> books = new List<Book>();
            _bookDaoMock.Setup(b => b.GetBooksByPublishingCompany(It.IsAny<string>())).Returns(books);
            // Act

            IEnumerable<IGrouping<string, Book>> result = _bookLogic.GetBooksByPublishingCompany("NotFoundedCompany");
            //Assert

            Assert.AreEqual(books, result);
        }

    }
}
