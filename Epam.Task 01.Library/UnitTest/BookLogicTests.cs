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
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            var expectedValidationResuilCount = validationObjects.Count;
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.AddBook(It.IsAny<Book>()))
                .Callback<Book>(book => books.Add(book));
            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);
            _bookDaoMock.Setup(b => b.CheckBookUniqueness(It.IsAny<Book>())).Returns(true);

            _bookValidationMock.Setup(s => s.IsValid).Returns(true);
            _bookValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _bookValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckBookCity(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckISBN(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Book>())).Returns(_bookValidationMock.Object);

            // Act

            _bookLogic.AddBook(validationObjects, _defaultBookItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsTrue(books.Contains(_defaultBookItem));
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void AddBook_AddingNotValidBook_ReturnFalse()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            var expectedValidationResuilCount = validationObjects.Count;
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.AddBook(It.IsAny<Book>()))
                .Callback<Book>(book => books.Add(book));
            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);
            _bookDaoMock.Setup(b => b.CheckBookUniqueness(It.IsAny<Book>())).Returns(false);

            _bookValidationMock.Setup(s => s.IsValid).Returns(true);
            _bookValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _bookValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckBookCity(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckISBN(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Book>())).Returns(_bookValidationMock.Object);

            // Act

            _bookLogic.AddBook(validationObjects, _defaultBookItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsTrue(books.Contains(_defaultBookItem));
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
