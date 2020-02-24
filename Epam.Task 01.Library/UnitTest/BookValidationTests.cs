using AbstractValidation;
using CollectionValidation;
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
    public class BookValidationTests
    {
        private IBookValidation _bookValidation;
        private Book _defaultBookItem;
        private Mock<ICommonValidation> _commonValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidationMock = new Mock<ICommonValidation>();
            _bookValidation = new BookValidation(_commonValidationMock.Object);

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
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRus_ReturnTrue()
        {
            // Arrange

            string city = "Саратов";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count; // и сколько ты ожидаешь получить?

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEng_ReturnTrue()
        {
            // Arrange

            string city = "Saratov";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithHyphen_ReturnTrue()
        {
            // Arrange

            string city = "Ростов-Ростов";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithHyphen_ReturnTrue()
        {
            // Arrange

            string city = "Rostov-Rostov";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithTwoHyphen_ReturnTrue()
        {
            // Arrange

            string city = "Ростов-на-Дону";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithTwoHyphens_ReturnTrue()
        {
            // Arrange

            string city = "Rostov-na-Donu";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithWhiteSpaces_ReturnTrue()
        {
            // Arrange

            string city = "Ростов на Дону";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithWhiteSpaces_ReturnTrue()
        {
            // Arrange

            string city = "Rostov na Donu";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithULowerCaseLetterLengthLessWhen200OnEng_ReturnFalse()
        {
            // Arrange

            string city = "city";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1 ;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithULowerCaseLetterLengtManyWordshLessWhen200OnEng_ReturnFalse()
        {
            // Arrange

            string city = "city city";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityStartWithEngAndRusWordsLessWhen200OnEng_ReturnFalse()
        {
            // Arrange

            string city = "Рус Eng";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityWithManyHyphensLessWhen200OnEng_ReturnFalse()
        {
            // Arrange

            string city = "Test-test-test-Test";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckBookCity_CityWithManyHyphensLessWhen200OnRus_ReturnFalse()
        {
            // Arrange

            string city = "Тест-тест-тест-Тест";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.City = city;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_IsNull_ReturnFalse()
        {
            // Arrange

            _defaultBookItem.City = null;
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            var validation = _bookValidation.CheckPublishingCompany(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckCommentary_StringLengthMoreThan200_ReturnFalse()
        {
            // Arrange

            int inputlength = 201;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < inputlength; i++)
            {
                stringBuilder.Append("*");
            }
            string commentaryText = stringBuilder.ToString();
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.Commentary = commentaryText;
            var validation = _bookValidation.CheckBookCity(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceAndWith10Number_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 0-1-1234567-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceCountryStartWith80AndWith10Number_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 80-1-123457-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceCountryStartWith950AndWith10Number_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 950-1-12347-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceCountryStartWith9940AndWith10Number_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 9940-1-1237-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceCountryStartWith99900AndWith10Number_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 99900-1-123-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceAndWith9NumberAndX_Returntrue()
        {
            // Arrange

            string isbn = "ISBN 0-1-1234567-X";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckISBN_AnyStringsStartWithISBNWhiteSpaceAndWithMoreThan10Number_ReturnFalse()
        {
            // Arrange

            string isbn = "ISBN 0-1-123456-3";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.isbn = isbn;
            var validation = _bookValidation.CheckISBN(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISBNLength_Length10_ReturnTrue()
        {
            // Arrange

            string input = "ISBN 0123456789";

            // Act

            bool result = _bookValidation.CheckISBNLessThanBottomBorderISBNLength(input);

            //Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckISBNLength_LengthNot10_ReturnFalse()
        {
            // Arrange

            string input = "ISBN 11111111111";

            // Act

            bool result = _bookValidation.CheckISBNLessThanBottomBorderISBNLength(input);

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckPublishingCompany_AnyStringLessThan300InRus_ReturnTrue()
        {
            // Arrange

            string company = "Моя книга";
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;
            _commonValidationMock.Setup(m => m.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            // Act

            _defaultBookItem.PublishingCompany = company;
            var validation = _bookValidation.CheckPublishingCompany(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_AnyStringMoreThan300InRus_ReturnFalse()
        {
            // Arrange
            int inputlength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < inputlength; i++)
            {
                stringBuilder.Append("*");
            }
            string text = stringBuilder.ToString();
            int currentValidationResultCount = _bookValidation.ValidationResult.Count;
            int expectedValidationResuilCount = currentValidationResultCount + 1;
            // Act
            _defaultBookItem.PublishingCompany = text;
            var validation = _bookValidation.CheckPublishingCompany(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_IsNull_ReturnFalse()
        {
            // Arrange
            _defaultBookItem.PublishingCompany = null;
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;
            // Act
            var validation = _bookValidation.CheckPublishingCompany(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckYearOfPublishing_YearMoreThan1400AndNotMoreCurrentYear_ReturnTrue()
        {
            // Arrange

            int year = 1500;
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;
            _commonValidationMock.Setup(m => m.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);
            
            // Act

            _defaultBookItem.YearOfPublishing = year;
            var validation = _bookValidation.CheckYearOfPublishing(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;
            
            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckYearOfPublishing_YearMoreLess1400_ReturnFalse()
        {
            // Arrange

            int year = 1399;
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.YearOfPublishing = year;
            var validation = _bookValidation.CheckYearOfPublishing(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckYearOfPublishing_YearMoreCurrentYear_ReturnFalse()
        {
            // Arrange

            int year = DateTime.Now.Year + 1;
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.YearOfPublishing = year;
            var validation = _bookValidation.CheckYearOfPublishing(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckAuthor_NameAndLasNameStartWithUpperCaseEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name", "Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }


        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStartWithUpperCaseEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name-Name", "Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenStartWithUpperCaseEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name-Name", "Lastname-Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenAndFamilyPrefixStartWithUpperCaseEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name-Name", "lastname Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStarWithLowerCaseWithFamilyPrefixEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name-Name", "lastname'Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStarWithUpperCaseWithFamilyPrefixEng_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Name-Name", "Lastname'Lastname");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameAndLasNameStartWithUpperCaseRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест", "Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }


        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStartWithUpperCaseRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест-Тест", "Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenStartWithUpperCaseRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест-Тест", "Тест-Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenAndFamilyPrefixStartWithUpperCaseRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест-Тест", "тест Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStarWithLowerCaseWithFamilyPrefixRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест-Тест", "тест'Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameWithHyphenAndLasNameStarWithUpperCaseWithFamilyPrefixRus_ReturnTrue()
        {
            // Arrange

            Author author = new Author("Тест-Тест", "Тест'Тест");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameAndLastNameStartWithLowerCaseeEng_ReturnFalse()
        {
            // Arrange

            Author author = new Author("name", "name");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count+1;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_NameAndLastNameStartHyphenLowerCaseeEng_ReturnFalse()
        {
            // Arrange

            Author author = new Author("-Name-", "-Lame-");
            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.Authors[0] = author;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckAuthor_AuthorisNull_ReturnFalse()
        {
            // Arrange

            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count + 1;

            // Act

            _defaultBookItem.Authors = null;
            var validation = _bookValidation.CheckAuthors(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckByCommonValidation_ValidData_ReturnTrue()
        {
            // Arrange

            int expectedValidationResuilCount = _bookValidation.ValidationResult.Count;
            _commonValidationMock.Setup(s => s.IsValid).Returns(true);
            _commonValidationMock.Setup(s => s.ValidationResult).Returns(_bookValidation.ValidationResult);
            _commonValidationMock.Setup(s => s.CheckTitle(_defaultBookItem)).Returns(_commonValidationMock.Object);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultBookItem)).Returns(_commonValidationMock.Object);

            // Act

            _defaultBookItem.Title = "Validtitle";
            _defaultBookItem.PagesCount = 10;
            var validation = _bookValidation.CheckByCommonValidation(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckByCommonValidation_NotValidData_ReturnFalse()
        {
            // Arrange

            _commonValidationMock.Setup(s => s.IsValid).Returns(false);
            List<ValidationObject> validationObjects = new List<ValidationObject>() { new ValidationObject("", ""), new ValidationObject("", "") };
            int expectedValidationResuilCount = validationObjects.Count;
            _commonValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _commonValidationMock.Setup(s => s.CheckTitle(_defaultBookItem)).Returns(_commonValidationMock.Object);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultBookItem)).Returns(_commonValidationMock.Object);
            int inputlength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < inputlength; i++)
            {
                stringBuilder.Append("*");
            }
            string Text = stringBuilder.ToString();
            // Act

            _defaultBookItem.Title = Text;
            _defaultBookItem.PagesCount = 300;
            var validation = _bookValidation.CheckByCommonValidation(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckByCommonValidation_TitleIsNull_ReturnFalse()
        {
            // Arrange

            _commonValidationMock.Setup(s => s.IsValid).Returns(false);
            List<ValidationObject> validationObjects = new List<ValidationObject>() { new ValidationObject("", ""), new ValidationObject("", "") };
            int expectedValidationResuilCount = validationObjects.Count;
            _commonValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _commonValidationMock.Setup(s => s.CheckTitle(_defaultBookItem)).Returns(_commonValidationMock.Object);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultBookItem)).Returns(_commonValidationMock.Object);

            // Act

            _defaultBookItem.Title = null;
            _defaultBookItem.PagesCount = 300;
            var validation = _bookValidation.CheckByCommonValidation(_defaultBookItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
