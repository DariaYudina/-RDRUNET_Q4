using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionDAL;
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
        private Issue _defaultNewspaperItem;

        private NewspaperLogic _newspaperLogic;
        private Mock<INewspaperDao> _newspaperDaoMock;
        private Mock<INewspaperValidation> _newspaperValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _newspaperValidationMock = new Mock<INewspaperValidation>();
            _newspaperDaoMock = new Mock<INewspaperDao>();
            _newspaperLogic = new NewspaperLogic(_newspaperDaoMock.Object, _newspaperValidationMock.Object);

            Issue defaultNewspaperItem = new Issue
            (
                newspaper: new Newspaper("","","",""),
                yearOfPublishing: 2000,
                countOfPublishing: 0,
                dateOfPublishing: DateTime.Now,
                pageCount: 1,
                commentary: ""
            );

            _defaultNewspaperItem = defaultNewspaperItem;
        }

        [TestMethod]
        public void AddNewspaper_AddingValidNewspaper_ReturnTrue()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Issue> newspapers = new List<Issue>();

            _newspaperDaoMock.Setup(b => b.AddNewspaper(It.IsAny<Issue>()))
                .Callback<Issue>(n => newspapers.Add(n));
            _newspaperDaoMock.Setup(b => b.GetNewspaperItems()).Returns(newspapers);

            _newspaperValidationMock.Setup(s => s.IsValid).Returns(true);
            _newspaperValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckByIssueValidation(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckCountOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckDateOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);

            // Act

            _newspaperLogic.AddNewspaper(validationObjects, _defaultNewspaperItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsTrue(newspapers.Contains(_defaultNewspaperItem));
        }

        [TestMethod]
        public void AddNewspaper_AddingNotValidNewspaper_ReturnFalse()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Issue> newspapers = new List<Issue>();

            _newspaperDaoMock.Setup(b => b.GetNewspaperItems()).Returns(newspapers);

            _newspaperValidationMock.Setup(s => s.IsValid).Returns(false);
            _newspaperValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _newspaperValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckByIssueValidation(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckCountOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckDateOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);
            _newspaperValidationMock.Setup(s => s.CheckYearOfPublishing(It.IsAny<Issue>())).Returns(_newspaperValidationMock.Object);

            // Act

            _newspaperLogic.AddNewspaper(validationObjects, _defaultNewspaperItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsFalse(newspapers.Contains(_defaultNewspaperItem));
        }

        [TestMethod]
        public void GetNewspaperItems_GetAnyNewspapersInNotEmptyDao_ReturnNewspaperItemsList()
        {
            // Arrange

            List<Issue> newspapers = new List<Issue>() { _defaultNewspaperItem, _defaultNewspaperItem, _defaultNewspaperItem };
            _newspaperDaoMock.Setup(b => b.GetNewspaperItems()).Returns(newspapers);

            // Act

            List<Issue> actualNewspapers = _newspaperLogic.GetNewspaperItems().ToList();

            //Assert

            CollectionAssert.AreEqual(newspapers, actualNewspapers);
        }

        [TestMethod]
        public void GetNewspaperItems_GetAnyNewspapersInEmptyDao_ReturnEmptyList()
        {
            // Arrange

            List<Issue> newspapers = new List<Issue>() { };
            _newspaperDaoMock.Setup(b => b.GetNewspaperItems()).Returns(newspapers);

            // Act

            List<Issue> actualNewspapers = _newspaperLogic.GetNewspaperItems().ToList();

            //Assert

            CollectionAssert.AreEqual(newspapers, actualNewspapers);
        }

    }
}
