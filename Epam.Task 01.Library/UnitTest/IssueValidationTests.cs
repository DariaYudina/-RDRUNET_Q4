using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
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
        private IIssueValidation _newspaperValidation;
        private Issue _defaultNewspaperItem;
        private Mock<ICommonValidation> _commonValidationMock;
        private Mock<INewspaperValidation> _issueValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _commonValidationMock = new Mock<ICommonValidation>();
            _issueValidationMock = new Mock<INewspaperValidation>();
            _newspaperValidation = new NewspaperValidation(_commonValidationMock.Object, _issueValidationMock.Object);

            Issue defaultNewspaperItem = new Issue
            (newspaper: new Newspaper
              (title: "",
               city: "",
               publishingCompany: "",
               issn: ""
              ),
              yearOfPublishing: 2000,
              countOfPublishing: 0,
              dateOfPublishing: DateTime.Now,
              pageCount: 0,
              commentary: ""
            );

            _defaultNewspaperItem = defaultNewspaperItem;
        }

        [TestMethod]
        public void CheckByCommonValidation_ValidData_ReturnTrue()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count;
            _commonValidationMock.Setup(s => s.IsValid).Returns(true);
            _commonValidationMock.Setup(s => s.ValidationResult).Returns(_newspaperValidation.ValidationResult);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultNewspaperItem)).Returns(_commonValidationMock.Object);

            // Act

            _defaultNewspaperItem.PagesCount = 10;
            var validation = _newspaperValidation.CheckByCommonValidation(_defaultNewspaperItem);
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
            _commonValidationMock.Setup(s => s.CheckTitle(_defaultNewspaperItem)).Returns(_commonValidationMock.Object);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultNewspaperItem)).Returns(_commonValidationMock.Object);
            int inputlength = 301;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < inputlength; i++)
            {
                stringBuilder.Append("*");
            }
            string Text = stringBuilder.ToString();
            // Act

            _defaultNewspaperItem.Title = Text;
            _defaultNewspaperItem.PagesCount = 300;
            var validation = _newspaperValidation.CheckByCommonValidation(_defaultNewspaperItem);
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
            _commonValidationMock.Setup(s => s.CheckTitle(_defaultNewspaperItem)).Returns(_commonValidationMock.Object);
            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultNewspaperItem)).Returns(_commonValidationMock.Object);

            // Act

            _defaultNewspaperItem.Title = null;
            _defaultNewspaperItem.PagesCount = 300;
            var validation = _newspaperValidation.CheckByCommonValidation(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckByIssueValidation_ValidData_ReturnTrue()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count;
            _issueValidationMock.Setup(s => s.IsValid).Returns(true);
            _issueValidationMock.Setup(s => s.ValidationResult).Returns(_newspaperValidation.ValidationResult);
            _issueValidationMock.Setup(s => s.CheckTitle(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckISSN(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckNewspaperCity(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckPublishingCompany(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);

            // Act

            _defaultNewspaperItem.Title = "Title";
            _defaultNewspaperItem.Newspaper.Issn = "Title";
            _defaultNewspaperItem.Newspaper.City = "City";
            _defaultNewspaperItem.Newspaper.PublishingCompany = "PublishingCompany";
            var validation = _newspaperValidation.CheckByNewspaperValidation(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckByIssueValidation_NotValidData_ReturnFalse()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 4;
            _issueValidationMock.Setup(s => s.IsValid).Returns(false);
            _issueValidationMock.Setup(s => s.ValidationResult).Returns(_newspaperValidation.ValidationResult);
            _issueValidationMock.Setup(s => s.CheckTitle(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckISSN(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckNewspaperCity(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckPublishingCompany(_defaultNewspaperItem.Newspaper)).Returns(_issueValidationMock.Object);

            // Act

            _defaultNewspaperItem.Title = "";
            _defaultNewspaperItem.Newspaper.Issn = "";
            _defaultNewspaperItem.Newspaper.City = "";
            _defaultNewspaperItem.Newspaper.PublishingCompany = "";
            var validation = _newspaperValidation.CheckByNewspaperValidation(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count + 4;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckCountOfPublishing_NotNegativeNaturalNumber_ReturnTrue()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            // Act

            _defaultNewspaperItem.CountOfPublishing = 1;
            var validation = _newspaperValidation.CheckCountOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckCountOfPublishing_isZero_ReturnFalse()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

            // Act

            _defaultNewspaperItem.CountOfPublishing = 0;
            var validation = _newspaperValidation.CheckCountOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count ;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckCountOfPublishing_isNegativeNumber_ReturnFalse()
        {
            // Arrange

            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

            // Act

            _defaultNewspaperItem.CountOfPublishing = -2;
            var validation = _newspaperValidation.CheckCountOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
        
        [TestMethod]
        public void CheckDateOfPublishing_DateOfPublishingYearEqualYear_ReturnTrue()
        {
            // Arrange

            _defaultNewspaperItem.DateOfPublishing = new DateTime(2000, 1, 1);
            _defaultNewspaperItem.YearOfPublishing = _defaultNewspaperItem.DateOfPublishing.Year;
            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            // Act

            var validation = _newspaperValidation.CheckDateOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckDateOfPublishing_DateOfPublishingYearNotEqualYear_ReturnFalse()
        {
            // Arrange

            _defaultNewspaperItem.DateOfPublishing = new DateTime(2000, 1, 1);
            _defaultNewspaperItem.YearOfPublishing = _defaultNewspaperItem.DateOfPublishing.Year + 1;
            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

            // Act

            var validation = _newspaperValidation.CheckDateOfPublishing(_defaultNewspaperItem);
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

            _defaultNewspaperItem.YearOfPublishing = DateTime.Now.Year;
            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

            // Act

            var validation = _newspaperValidation.CheckDateOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckYearOfPublishing_YearLessThan1400_ReturnFalse()
        {
            // Arrange

            _defaultNewspaperItem.YearOfPublishing = 1399;
            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

            // Act

            var validation = _newspaperValidation.CheckDateOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }

        [TestMethod]
        public void CheckYearOfPublishing_YearMoreThanCurrentYear_ReturnFalse()
        {
            // Arrange

            _defaultNewspaperItem.YearOfPublishing = DateTime.Now.Year + 1;
            int expectedValidationResuilCount = _newspaperValidation.ValidationResult.Count + 1;
            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

            // Act

            var validation = _newspaperValidation.CheckDateOfPublishing(_defaultNewspaperItem);
            bool result = validation.IsValid;
            int actualValidationResuilCount = validation.ValidationResult.Count;

            //Assert

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
        }
    }

}
