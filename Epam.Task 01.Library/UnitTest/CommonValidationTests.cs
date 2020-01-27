using System;
using System.Collections.Generic;
using System.Text;
using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class CommonValidationTests
    {
        ICommonValidation CommonValidation;
        AbstractLibraryItem DefaultAbstractLibraryItem;

        [TestInitialize]
        public void Initialize()
        {
            CommonValidation = new CommonValidation();

            AbstractLibraryItem defaultAbstractLibraryItem = new Book 
            ( authors: new List<Author>() { new Author("", "") },
              city : "",
              publishingCompany :"",
              yearOfPublishing : 0,
              isbn : "",
              title : "",
              pagesCount : 0, 
              commentary : ""
            );

            DefaultAbstractLibraryItem = defaultAbstractLibraryItem;
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineAndBottomLineChangedPlaces_ReturnTrue()
        {
            // Arrange

            int? bottomline = 0;
            int? timberline = -2;
            int number = -1;

            // Act
            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineLessThanValueAndBottomLineNull_ReturnFalse()
        {
            // Arrange

            int? bottomline = null;
            int? timberline = -1;
            int number = 0;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_BottomLineMoreThanValueAndTimberLineNull_ReturnFalse()
        {
            // Arrange

            int? bottomline = 1;
            int? timberline = null;
            int number = 0;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_BottomLineIsNull_ReturnTrue()
        {
            // Arrange

            int? bottomline = null;
            int timberline = 1;
            int number = 0;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineIsNull_ReturnTrue()
        {
            // Arrange

            int? bottomline = 0;
            int? timberline = null;
            int number = 1;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineAndBottomLineIsNull_ReturnTrue()
        {
            // Arrange

            int? bottomline = null;
            int? timberline = null;
            int number = 0;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckNumericalInRange_TimberLineAndBottomLineIsNumber_ReturnTrue()
        {
            // Arrange

            int? bottomline = -1;
            int? timberline = 1;
            int number = 0;

            // Act

            bool result = CommonValidation.CheckNumericalInRange(number, timberline, bottomline);

            // Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckCommentary_StringLength10_ReturnTrue()
        {
            // Arrange

            string commentaryText = "0123456789";
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            // Act

            DefaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = CommonValidation.CheckCommentary(DefaultAbstractLibraryItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

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
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;

            // Act

            DefaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = CommonValidation.CheckCommentary(DefaultAbstractLibraryItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
           
        }

        [TestMethod]
        public void CheckCommentary_CommentaryIsOptionalField_StringIsNull_ReturnTrue()
        {
            // Arrange

            string commentaryText = null;
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            // Act

            DefaultAbstractLibraryItem.Commentary = commentaryText;
            var validation = CommonValidation.CheckCommentary(DefaultAbstractLibraryItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIs100_ReturnTrue()
        {
            // Arrange

            int pagesCount = 100;
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            // Act

            DefaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = CommonValidation.CheckPagesCount(DefaultAbstractLibraryItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIsZero_ReturnTrue()
        {
            // Arrange

            int pagesCount = 0;
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            // Act

            DefaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = CommonValidation.CheckPagesCount(DefaultAbstractLibraryItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckPagesCount_PageCountIsNegative_ReturnFalse()
        {
            // Arrange

            int pagesCount = -1;
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;

            // Act

            DefaultAbstractLibraryItem.PagesCount = pagesCount;
            var validation = CommonValidation.CheckPagesCount(DefaultAbstractLibraryItem);
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
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            //Act

            DefaultAbstractLibraryItem.Title = title;
            var validation = CommonValidation.CheckTitle(DefaultAbstractLibraryItem);
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

            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;
            int titleLength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < titleLength; i++)
            {
                stringBuilder.Append("*");
            }
            string titleText = stringBuilder.ToString();

            //Act

            DefaultAbstractLibraryItem.Title = titleText;
            var validation = CommonValidation.CheckTitle(DefaultAbstractLibraryItem);
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
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;

            //Act

            DefaultAbstractLibraryItem.Title = title;
            var validation = CommonValidation.CheckTitle(DefaultAbstractLibraryItem);
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
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count;

            //Act

            CommonValidation.CheckStringIsNotNullorEmpty(text);
            bool result = CommonValidation.IsValid;
            int actualValidationResuilCount = CommonValidation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisNull_ReturnFalse()
        {
            // Arrange 

            string text = null;
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;

            //Act

            CommonValidation.CheckStringIsNotNullorEmpty(text);
            bool result = CommonValidation.IsValid;
            int actualValidationResuilCount = CommonValidation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckStringIsNotNullorEmpty_StringisEmpty_ReturnFalse()
        {
            // Arrange 

            string text = "   ";
            int expectedValidationResuilCount = CommonValidation.ValidationResult.Count + 1;

            //Act

            CommonValidation.CheckStringIsNotNullorEmpty(text);
            bool result = CommonValidation.IsValid;
            int actualValidationResuilCount = CommonValidation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }
}
