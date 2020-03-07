using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
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
    public class NewspaperLogicTests
    {
        private Newspaper _defaultIssueItem;

        private NewspaperLogic _issueLogic;
        private Mock<INewspaperDao> _issueDaoMock;
        private Mock<INewspaperValidation> _newspaperValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _newspaperValidationMock = new Mock<INewspaperValidation>();
            _issueDaoMock = new Mock<INewspaperDao>();
            _issueLogic = new NewspaperLogic(_issueDaoMock.Object, _newspaperValidationMock.Object);

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
        public void AddIssue_AnyValidIssue_ReturnTrue()
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Newspaper> issues = new List<Newspaper>();

            _issueDaoMock.Setup(b => b.AddNewspaper(It.IsAny<Newspaper>()))
                .Callback<Newspaper>(issue => issues.Add(issue));
            _issueDaoMock.Setup(b => b.GetNewspaperItems()).Returns(issues);

            _newspaperValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(true);
            _newspaperValidationMock.Setup(s => s.ValidationObject).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);

            // Act

            _issueLogic.AddNewspaper(out validationObjects, _defaultIssueItem);
            var actualValidationResuilCount = validationObjects.ValidationExceptions.Count;

            //Assert

            Assert.IsTrue(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void AddIssue_AnyValidIssue_ReturnFalse()
        {
            // Arrange
            ValidationObject validationObjects = new ValidationObject();
            List<Newspaper> issues = new List<Newspaper>();

            _issueDaoMock.Setup(b => b.GetNewspaperItems()).Returns(issues);

            _newspaperValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(false);
            _newspaperValidationMock.Setup(s => s.ValidationObject).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_newspaperValidationMock.Object);

            // Act

            _issueLogic.AddNewspaper(out validationObjects, _defaultIssueItem);
            var actualValidationResuilCount = validationObjects.ValidationExceptions.Count;

            //Assert

            Assert.IsFalse(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void GetIssueItemById__IdFoundedBookInCollection_ReturnIssue()
        {
            _issueDaoMock.Setup(b => b.GetNewspaperItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act

            var resultBook = _issueLogic.GetNewspaperItemById(5);

            //Assert

            Assert.IsNotNull(resultBook);
        }

        [TestMethod]
        public void GetIssueItemById__IdNotFoundedBookInCollection_ReturnNull()
        {
            _issueDaoMock.Setup(b => b.GetNewspaperItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act

            var resultBook = _issueLogic.GetNewspaperItemById(11);

            //Assert

            Assert.IsNull(resultBook);
        }
    }
}
