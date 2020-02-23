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
    public class IssueLogicTests
    {
        private Newspaper _defaultIssueItem;

        private IssueLogic _issueLogic;
        private Mock<IIssueDao> _issueDaoMock;
        private Mock<IIssueValidation> _issueValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _issueValidationMock = new Mock<IIssueValidation>();
            _issueDaoMock = new Mock<IIssueDao>();
            _issueLogic = new IssueLogic(_issueDaoMock.Object, _issueValidationMock.Object);

            Newspaper defaultIssueItem = new Newspaper
            (
              title: "",
              city: "",
              publishingCompany: "",
              issn:""
            );

            _defaultIssueItem = defaultIssueItem;
        }

        [TestMethod]
        public void AddIssue_AnyValidIssue_ReturnTrue()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Newspaper> issues = new List<Newspaper>();

            _issueDaoMock.Setup(b => b.AddIssue(It.IsAny<Newspaper>()))
                .Callback<Newspaper>(issue => issues.Add(issue));
            _issueDaoMock.Setup(b => b.GetIssueItems()).Returns(issues);

            _issueValidationMock.Setup(s => s.IsValid).Returns(true);
            _issueValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _issueValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);

            // Act

            _issueLogic.AddIssue(validationObjects, _defaultIssueItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsTrue(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void AddIssue_AnyValidIssue_ReturnFalse()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Newspaper> issues = new List<Newspaper>();

            _issueDaoMock.Setup(b => b.GetIssueItems()).Returns(issues);

            _issueValidationMock.Setup(s => s.IsValid).Returns(false);
            _issueValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _issueValidationMock.Setup(s => s.CheckISSN(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckNewspaperCity(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckPublishingCompany(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);
            _issueValidationMock.Setup(s => s.CheckTitle(It.IsAny<Newspaper>())).Returns(_issueValidationMock.Object);

            // Act

            _issueLogic.AddIssue(validationObjects, _defaultIssueItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsFalse(issues.Contains(_defaultIssueItem));
        }

        [TestMethod]
        public void GetIssueItemById__IdFoundedBookInCollection_ReturnIssue()
        {
            _issueDaoMock.Setup(b => b.GetIssueItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act

            var resultBook = _issueLogic.GetIssueItemById(5);

            //Assert

            Assert.IsNotNull(resultBook);
        }

        [TestMethod]
        public void GetIssueItemById__IdNotFoundedBookInCollection_ReturnNull()
        {
            _issueDaoMock.Setup(b => b.GetIssueItemById(It.IsInRange(1, 10, Range.Inclusive))).Returns(_defaultIssueItem);

            // Act

            var resultBook = _issueLogic.GetIssueItemById(11);

            //Assert

            Assert.IsNull(resultBook);
        }
    }
}
