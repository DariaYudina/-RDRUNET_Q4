using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.CollectionBLL.Validators;
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
    public class IssueValidationTests
    {
        private IIssueValidation _issueValidation;
        private Issue _defaultIssueItem;
        private Mock<ICommonValidation> _commonValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidationMock = new Mock<ICommonValidation>();
            _issueValidation = new IssueValidation(_commonValidationMock.Object);

            Issue defaultIssueItem = new Issue
            ( title: "",
              city: "",
              publishingCompany:"",
              issn: ""
            );

            _defaultIssueItem = defaultIssueItem;
        }
        
        [TestMethod]
        public void CheckISSN_ISSNAnd8Number_ReturnTrue()
        {
            // Arrange

            _defaultIssueItem.Issn = "ISSN 1234-1234";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            var validation = _issueValidation.CheckISSN(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckISSN_ISSNAndNot8Number_ReturnFalse()
        {
            // Arrange

            _defaultIssueItem.Issn = "ISSN 1234-123";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            var validation = _issueValidation.CheckISSN(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckNewspaperCity_ISSNAnd8Number_ReturnTrue()
        {
            // Arrange

            _defaultIssueItem.City = "City";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRus_ReturnTrue()
        {
            // Arrange

            string city = "Саратов";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithTwoHyphen_ReturnFalse()
        {
            // Arrange

            string city = "Ростов-на-Дону";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnEngWithTwoHyphens_ReturnTrue()
        {
            // Arrange

            string city = "Rostov-na-Donu";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckBookCity_CityStartWithUpperCaseLetterLengthLessWhen200OnRusWithWhiteSpaces_ReturnTrue()
        {
            // Arrange

            string city = "Ростов на Дону";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
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
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckPublishingCompany_n_ReturnFalse()
        {
            // Arrange

            string city = "Тест-тест-тест-Тест";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            // Act

            _defaultIssueItem.City = city;
            var validation = _issueValidation.CheckNewspaperCity(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPublishingCompany_AnyStringLessThan300InRus_ReturnTrue()
        {
            // Arrange

            string company = "Моя книга";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;
            _commonValidationMock.Setup(m => m.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            // Act

            _defaultIssueItem.PublishingCompany = company;
            var validation = _issueValidation.CheckPublishingCompany(_defaultIssueItem);
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
            int currentValidationResultCount = _issueValidation.ValidationResult.Count;
            int expectedValidationResuilCount = currentValidationResultCount + 1;

            // Act

            _defaultIssueItem.PublishingCompany = text;
            var validation = _issueValidation.CheckPublishingCompany(_defaultIssueItem);
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

            _defaultIssueItem.PublishingCompany = null;
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;
            
            // Act
            
            var validation = _issueValidation.CheckPublishingCompany(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;
            
            //Assert
            
            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_Titleis10Length_ReturnTrue()
        {
            // Arrange 

            string title = "0123456789";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;
            _commonValidationMock.Setup(m => m.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            //Act

            _defaultIssueItem.Title = title;
            var validation = _issueValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_TitleisLengthMore300_ReturnFalse()
        {
            // Arrange 

            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;
            int titleLength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < titleLength; i++)
            {
                stringBuilder.Append("*");
            }
            string titleText = stringBuilder.ToString();


            //Act

            _defaultIssueItem.Title = titleText;
            var validation = _issueValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckTitle_TitleisNull_ReturnFalse()
        {
            // Arrange 

            string title = null;
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(m => m.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);
            //Act

            _defaultIssueItem.Title = title;
            var validation = _issueValidation.CheckTitle(_defaultIssueItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisNotNull_ReturnTrue()
        {
            // Arrange 

            string text = "test";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count;

            //Act

            _issueValidation.CheckStringIsNullorEmpty(text);
            bool result = _issueValidation.IsValid;
            int actualValidationResuilCount = _issueValidation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisNull_ReturnFalse()
        {
            // Arrange 

            string text = null;
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            //Act

            _issueValidation.CheckStringIsNullorEmpty(text);
            bool result = _issueValidation.IsValid;
            int actualValidationResuilCount = _issueValidation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisEmpty_ReturnFalse()
        {
            // Arrange 

            string text = "   ";
            int expectedValidationResuilCount = _issueValidation.ValidationResult.Count + 1;

            //Act

            _issueValidation.CheckStringIsNullorEmpty(text);
            bool result = _issueValidation.IsValid;
            int actualValidationResuilCount = _issueValidation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
