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
    public class CommonValidatorTests
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
            int? bottomline = 1;
            int? timberline = null;
            int number = 0;

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

            // Act
            DefaultAbstractLibraryItem.Commentary = commentaryText;
            bool result = CommonValidation.CheckCommentary(DefaultAbstractLibraryItem).IsValid;

            //Assert
            Assert.IsTrue(result);
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
            
            // Act
            DefaultAbstractLibraryItem.Commentary = commentaryText;
            bool result = CommonValidation.CheckCommentary(DefaultAbstractLibraryItem).IsValid;
            
            //Assert
            Assert.IsFalse(result);
        }
    }
}
