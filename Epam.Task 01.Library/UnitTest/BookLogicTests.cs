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
        public void AddBook_AddingValidBook_ReturnTrue()    //так кто в итоге вернул true?
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.AddBook(It.IsAny<Book>()))
                .Callback<Book>(book => books.Add(book));
            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);

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
        }

        [TestMethod]
        public void AddBook_AddingNotValidBook_ReturnFalse()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Book> books = new List<Book>();

            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);

            _bookValidationMock.Setup(s => s.IsValid).Returns(false);
            _bookValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _bookValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckBookCity(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckISBN(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Book>())).Returns(_bookValidationMock.Object);
            _bookValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Book>())).Returns(_bookValidationMock.Object);

            // Act

            _bookLogic.AddBook(validationObjects, _defaultBookItem);

            //Assert

            Assert.IsFalse(books.Contains(_defaultBookItem));
        }
        
        [TestMethod]
        public void GetBookById_IdFoundedBookInCollection_ReturnBook()
        {
            // Arrange
            _bookDaoMock.Setup(b => b.GetBookById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultBookItem);

            // Act

            var resultBook = _bookLogic.GetBookById(5);

            //Assert

            Assert.IsNotNull(resultBook);
        }

        [TestMethod]
        public void GetBookById_IdNotFoundedBookInCollection_ReturnNull()
        {
            // Arrange
            _bookDaoMock.Setup(b => b.GetBookById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultBookItem);

            // Act

            var resultBook = _bookLogic.GetBookById(11);

            //Assert

            Assert.IsNull(resultBook);
        }

        [TestMethod]
        public void GetBookItems_GetAnyBooksInNotEmptyDao_ReturnListWithBooks()
        {
            // Arrange
   
            List<Book> books = new List<Book>() { _defaultBookItem, _defaultBookItem, _defaultBookItem};
            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);

            // Act

            List<Book> actualBooks =  _bookLogic.GetBookItems().ToList();

            //Assert

            CollectionAssert.AreEqual(books, actualBooks);
        }

        [TestMethod]
        public void GetBookItems_GetAnyBookInEmptyDao_ReturnEmptyList()
        {
            // Arrange

            List<Book> books = new List<Book>();
            _bookDaoMock.Setup(b => b.GetBookItems()).Returns(books);

            // Act

            List<Book> actualBooks = _bookLogic.GetBookItems().ToList();

            //Assert

            CollectionAssert.AreEqual(books, actualBooks);
        }

        
        //[TestMethod]
        //public void CheckBookUniqueness_CheckUniquenessBooks_ReturnTrue()
        //{
        //    // Arrange

        //    _bookDaoMock.Setup(b => b.CheckBookUniqueness(It.IsAny<Book>())).Returns(true);

        //    // Act

        //    bool result = _bookLogic.CheckBookUniqueness(_defaultBookItem);

        //    //Assert

        //    Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public void CheckBookUniqueness_CheckNotUniquenessBooks_ReturnFalse()
        //{
        //    // Arrange

        //    _bookDaoMock.Setup(b => b.CheckBookUniqueness(It.IsAny<Book>())).Returns(false);

        //    // Act

        //    bool result = _bookLogic.CheckBookUniqueness(_defaultBookItem);

        //    //Assert

        //    Assert.IsFalse(result);
        //}

        
        [TestMethod]
        public void GetBooksByPublishingCompany_FoundedInDaoByPublishingCompany_ReturnIGroupingBooks()
        {
            // Arrange

            List<IGrouping<string, Book>> books = new List<IGrouping<string, Book>>();
           
            _bookDaoMock.Setup(b => b.GetBooksByPublishingCompany(It.IsAny<string>())).Returns(books);

            // Act

            IEnumerable<IGrouping<string, Book>> result = _bookLogic.GetBooksByPublishingCompany( "foundedCompany" );

            //Assert

            Assert.AreEqual(books, result);
        }

        [TestMethod]
        public void GetBooksByPublishingCompany_NotFoundedInDaoByPublishingCompany_ReturnEmptyIgroupingBooks()
        {
            // Arrange

            List<IGrouping<string, Book>> books = null;

            _bookDaoMock.Setup(b => b.GetBooksByPublishingCompany(It.IsAny<string>())).Returns(books);

            // Act

            IEnumerable<IGrouping<string, Book>> result = _bookLogic.GetBooksByPublishingCompany("NotFoundedCompany");

            //Assert

            Assert.AreEqual(books, result);
        }

    }
}
