using System.Text;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.CollectionBLL.Validators;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class NewspaperValidationTests
    {
        private INewspaperValidation _newspaperValidation;
        private Newspaper _defaultIssueItem;
        private Mock<ICommonValidation> _commonValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidationMock = new Mock<ICommonValidation>();
            _newspaperValidation = new NewspaperValidation(_commonValidationMock.Object);

            Newspaper defaultIssueItem = new Newspaper
            (
              title: "",
              city: "",
              publishingCompany: "",
              issn: ""
            );
            _defaultIssueItem = defaultIssueItem;
        }

        [TestMethod]
        public void CheckISSN_ISSNAnd8Number_ReturnTrue()
        {
            // Arrange
            _defaultIssueItem.Issn = "ISSN 1234-1234";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            INewspaperValidation validation = _newspaperValidation.CheckISSN(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISSN_ISSNAndNot8Number_ReturnFalse()
        {
            // Arrange
            _defaultIssueItem.Issn = "ISSN 1234-123";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            INewspaperValidation validation = _newspaperValidation.CheckISSN(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckNewspaperCity_ISSNAnd8Number_ReturnTrue()
        {
            // Arrange
            _defaultIssueItem.City = "City";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRus_ReturnTrue()
        {
            // Arrange
            string city = "Саратов";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEng_ReturnTrue()
        {
            // Arrange
            string city = "Saratov";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithHyphen_ReturnTrue()
        {
            // Arrange
            string city = "Ростов-Ростов";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithHyphen_ReturnTrue()
        {
            // Arrange
            string city = "Rostov-Rostov";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithTwoHyphen_ReturnFalse()
        {
            // Arrange
            string city = "Ростов-на-Дону";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithTwoHyphens_ReturnTrue()
        {
            // Arrange
            string city = "Rostov-na-Donu";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithWhiteSpaces_ReturnTrue()
        {
            // Arrange
            string city = "Ростов на Дону";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithWhiteSpaces_ReturnTrue()
        {
            // Arrange
            string city = "Rostov na Donu";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithULowerCaseLetterLengthLessWhen200OnEng_ReturnFalse()
        {
            // Arrange
            string city = "city";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithULowerCaseLetterLengtManyWordshLessWhen200OnEng_ReturnFalse()
        {
            // Arrange
            string city = "city city";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithEngAndRusWordsLessWhen200OnEng_ReturnFalse()
        {
            // Arrange
            string city = "Рус Eng";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityWithManyHyphensLessWhen200OnEng_ReturnFalse()
        {
            // Arrange
            string city = "Test-test-test-Test";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityWithManyHyphensLessWhen200OnRus_ReturnFalse()
        {
            // Arrange
            string city = "Тест-тест-тест-Тест";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_n_ReturnFalse()
        {
            // Arrange
            string city = "Тест-тест-тест-Тест";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            _defaultIssueItem.City = city;
            INewspaperValidation validation = _newspaperValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_AnyStringLessThan300InRus_ReturnTrue()
        {
            // Arrange
            string company = "Моя книга";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            // Act
            _defaultIssueItem.PublishingCompany = company;
            INewspaperValidation validation = _newspaperValidation.CheckPublishingCompany(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

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
            int currentValidationResultCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;
            int expectedValidationResuilCount = currentValidationResultCount + 1;

            // Act
            _defaultIssueItem.PublishingCompany = text;
            INewspaperValidation validation = _newspaperValidation.CheckPublishingCompany(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_IsNull_ReturnFalse()
        {
            // Arrange
            _defaultIssueItem.PublishingCompany = null;
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act
            INewspaperValidation validation = _newspaperValidation.CheckPublishingCompany(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_Titleis10Length_ReturnTrue()
        {
            // Arrange 
            string title = "0123456789";
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            //Act
            _defaultIssueItem.Title = title;
            INewspaperValidation validation = _newspaperValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_TitleisLengthMore300_ReturnFalse()
        {
            // Arrange 
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;
            int titleLength = 301;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < titleLength; i++)
            {
                stringBuilder.Append("*");
            }

            string titleText = stringBuilder.ToString();

            //Act
            _defaultIssueItem.Title = titleText;
            INewspaperValidation validation = _newspaperValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_TitleisNull_ReturnFalse()
        {
            // Arrange 
            string title = null;
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;
           
            //Act
            _defaultIssueItem.Title = title;
            INewspaperValidation validation = _newspaperValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisNotNull_ReturnTrue()
        {
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            //Act
            bool result = _newspaperValidation.ValidationObject.IsValid;
            int actualValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisNull_ReturnFalse()
        {
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            //Act
            bool result = _newspaperValidation.ValidationObject.IsValid;
            int actualValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisEmpty_ReturnFalse()
        {
            int expectedValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count + 1;

            //Act
            bool result = _newspaperValidation.ValidationObject.IsValid;
            int actualValidationResuilCount = _newspaperValidation.ValidationObject.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
