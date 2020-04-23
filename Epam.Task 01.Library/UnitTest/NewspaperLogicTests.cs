using System.Collections.Generic;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class NewspaperLogicTests
    {
        private Newspaper _defaultIssueItem;

        private NewspaperLogic _issueLogic;
        private Mock<INewspaperDao> _newspaperDaoMock;
        private Mock<INewspaperValidation> _newspaperValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _newspaperValidationMock = new Mock<INewspaperValidation>();
            _newspaperDaoMock = new Mock<INewspaperDao>();
            _issueLogic = new NewspaperLogic(_newspaperDaoMock.Object, _newspaperValidationMock.Object);

            Newspaper defaultIssueItem = new Newspaper(
              title: "",
              city: "",
              publishingCompany: "",
              issn: ""
            );
            _defaultIssueItem = defaultIssueItem;
        }

        [TestMethod]
        public void AddIssue_AnyValidIssue_ReturnTrue()
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Newspaper> issues = new List<Newspaper>();

            _newspaperDaoMock.Setup(b => b.AddNewspaper(It.IsAny<Newspaper>()))
                .Callback<Newspaper>(issue => issues.Add(issue));
            _newspaperDaoMock.Setup(b => b.GetNewspapers()).Returns(issues);

            _newspaperValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(true);
            _newspaperValidationMock.Setup(s => s.ValidationObject).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);

            // Act
            _issueLogic.AddNewspaper(out validationObjects, _defaultIssueItem);
            int actualValidationResuilCount = validationObjects.ValidationExceptions.Count;

            //Assert
            Assert.IsTrue(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void AddIssue_AnyValidIssue_ReturnFalse()
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Newspaper> issues = new List<Newspaper>();

            _newspaperDaoMock.Setup(b => b.GetNewspapers()).Returns(issues);

            _newspaperValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(false);
            _newspaperValidationMock.Setup(s => s.ValidationObject).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);

            // Act
            _issueLogic.AddNewspaper(out validationObjects, _defaultIssueItem);
            int actualValidationResuilCount = validationObjects.ValidationExceptions.Count;

            //Assert
            Assert.IsFalse(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void GetIssueItemById__IdFoundedBookInCollection_ReturnIssue()
        {
            _newspaperDaoMock.Setup(b => b.GetNewspaperById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act
            Newspaper resultBook = _issueLogic.GetNewspaperById(5);

            //Assert
            Assert.IsNotNull(resultBook);
        }

        [TestMethod]
        public void GetIssueItemById__IdNotFoundedBookInCollection_ReturnNull()
        {
            _newspaperDaoMock.Setup(b => b.GetNewspaperById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act
            Newspaper resultBook = _issueLogic.GetNewspaperById(11);

            //Assert
            Assert.IsNull(resultBook);
        }
    }
}
