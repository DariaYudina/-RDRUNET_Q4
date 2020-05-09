using System;
using System.Collections.Generic;
using System.Text;
using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
//using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class CommonValidationTests
    {
        private ICommonValidation _commonValidation;
        private AbstractLibraryItem _defaultAbstractLibraryItem;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidation = new CommonValidation();

            AbstractLibraryItem defaultAbstractLibraryItem = new Book
            (authors: new List<Author>() { new Author("", "") },
              city: "",
              publishingCompany: "",
              yearOfPublishing: 0,
              isbn: "",
              title: "",
              pagesCount: 0,
              commentary: ""
            );

            _defaultAbstractLibraryItem = defaultAbstractLibraryItem;
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineAndBottomLineChangedPlaces_ReturnTrue()
        {
            // Arrange

            int bottomline = 0;
            int timberline = -2;
            int number = -1;

            // Act
            bool result = _commonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineAndBottomLineIsNumber_ReturnTrue()
        {
            // Arrange

            int bottomline = -1;
            int timberline = 1;
            int number = 0;

            // Act

            bool result = _commonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckCommentary_StringLength10_ReturnTrue()
        {
            // Arrange

            string commentaryText = "0123456789";
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count;

            // Act

            _defaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = _commonValidation.CheckCommentary(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckCommentary_StringLength2001_ReturnFalse()
        {
            // Arrange

            int commentarylength = 2001;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < commentarylength; i++)
            {
                stringBuilder.Append("*");
            }
            string commentaryText = stringBuilder.ToString();
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act

            _defaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = _commonValidation.CheckCommentary(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);

        }

        [TestMethod]
        public void CheckCommentary_CommentaryIsOptionalField_StringIsNull_ReturnTrue()
        {
            // Arrange

            string commentaryText = null;
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count;

            // Act

            _defaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = _commonValidation.CheckCommentary(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIs100_ReturnTrue()
        {
            // Arrange

            int pagesCount = 100;
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count;

            // Act

            _defaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = _commonValidation.CheckPagesCount(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIsZero_ReturnTrue()
        {
            // Arrange

            int pagesCount = 0;
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count;

            // Act

            _defaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = _commonValidation.CheckPagesCount(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIsNegative_ReturnFalse()
        {
            // Arrange

            int pagesCount = -1;
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count + 1;

            // Act

            _defaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = _commonValidation.CheckPagesCount(_defaultAbstractLibraryItem);
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
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count;

            //Act

            _defaultAbstractLibraryItem.Title = title;
            var validation = _commonValidation.CheckTitle(_defaultAbstractLibraryItem);
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

            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count + 1;
            int titleLength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < titleLength; i++)
            {
                stringBuilder.Append("*");
            }
            string titleText = stringBuilder.ToString();

            //Act

            _defaultAbstractLibraryItem.Title = titleText;
            var validation = _commonValidation.CheckTitle(_defaultAbstractLibraryItem);
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
            int expectedValidationResuilCount = _commonValidation.ValidationObject.ValidationExceptions.Count + 1;

            //Act

            _defaultAbstractLibraryItem.Title = title;
            var validation = _commonValidation.CheckTitle(_defaultAbstractLibraryItem);
            bool result = validation.ValidationObject.IsValid;
            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
