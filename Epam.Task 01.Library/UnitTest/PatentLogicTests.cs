using AbstractValidation;
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
    public class PatentLogicTests
    {
        private Patent _defaultPatentItem;

        private PatentLogic _patentLogic;
        private Mock<IPatentDao> _patentDaoMock;
        private Mock<IPatentValidation> _patentValidationMock;

        [TestInitialize]
        public void Initialize()
        {
            _patentValidationMock = new Mock<IPatentValidation>();
            _patentDaoMock = new Mock<IPatentDao>();
            _patentLogic = new PatentLogic(_patentDaoMock.Object, _patentValidationMock.Object);

            Patent defaultPatentItem = new Patent
            (
                authors: new List<Author>() { new Author("", "") },
                country: "",
                registrationNumber: "",
                applicationDate: DateTime.Now,
                publicationDate: DateTime.Now,
                title: "",
                pageCount: 1,
                commentary: ""
            );

            _defaultPatentItem = defaultPatentItem;
        }

        [TestMethod]
        public void AddPatent_AddingValidPatent_ReturnTrue()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Patent> patents = new List<Patent>();

            _patentDaoMock.Setup(b => b.AddPatent(It.IsAny<Patent>()))
                .Callback<Patent>(n => patents.Add(n));
            _patentDaoMock.Setup(b => b.GetPatentItems()).Returns(patents);
            _patentDaoMock.Setup(b => b.CheckPatentUniqueness(It.IsAny<Patent>())).Returns(true);

            _patentValidationMock.Setup(s => s.IsValid).Returns(true);
            _patentValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _patentValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckCountry(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckRegistrationNumber(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckPublicationDate(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);

            // Act

            _patentLogic.AddPatent(validationObjects, _defaultPatentItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsTrue(patents.Contains(_defaultPatentItem));
        }

        [TestMethod]
        public void AddPatent_AddingNotValidPatent_ReturnFalse()
        {
            // Arrange
            List<ValidationObject> validationObjects = new List<ValidationObject>();
            List<Patent> patents = new List<Patent>();

            _patentDaoMock.Setup(b => b.GetPatentItems()).Returns(patents);
            _patentDaoMock.Setup(b => b.CheckPatentUniqueness(It.IsAny<Patent>())).Returns(false);

            _patentValidationMock.Setup(s => s.IsValid).Returns(false);
            _patentValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
            _patentValidationMock.Setup(s => s.CheckByCommonValidation(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckCountry(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckRegistrationNumber(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckPublicationDate(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);
            _patentValidationMock.Setup(s => s.CheckAuthors(It.IsAny<Patent>())).Returns(_patentValidationMock.Object);

            // Act

            _patentLogic.AddPatent(validationObjects, _defaultPatentItem);
            var actualValidationResuilCount = validationObjects.Count;

            //Assert

            Assert.IsFalse(patents.Contains(_defaultPatentItem));
        }

        [TestMethod]
        public void GetPatentItems_GetAnyPatentsInNotEmptyDao_ReturnPatentItemsList()
        {
            // Arrange

            List<Patent> patents = new List<Patent>() { _defaultPatentItem, _defaultPatentItem, _defaultPatentItem };
            _patentDaoMock.Setup(b => b.GetPatentItems()).Returns(patents);

            // Act

            List<Patent> actual = _patentLogic.GetPatentItems().ToList();

            //Assert

            CollectionAssert.AreEqual(patents, actual);
        }

        [TestMethod]
        public void GetPatentItems_GetAnyPatentsInEmptyDao_ReturnEmptyList()
        {
            // Arrange

            List<Patent> patents = new List<Patent>() { };
            _patentDaoMock.Setup(b => b.GetPatentItems()).Returns(patents);

            // Act

            List<Patent> actual = _patentLogic.GetPatentItems().ToList();

            //Assert

            CollectionAssert.AreEqual(patents, actual);
        }

        [TestMethod]
        public void CheckPatentUniqueness_CheckUniquenessPatents_ReturnTrue()
        {
            // Arrange

            _patentDaoMock.Setup(b => b.CheckPatentUniqueness(It.IsAny<Patent>())).Returns(true);

            // Act

            bool result = _patentLogic.CheckPatentUniqueness(_defaultPatentItem);

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckPatentUniqueness_ChecknotUniquenessPatents_ReturnFalse()
        {
            // Arrange

            _patentDaoMock.Setup(b => b.CheckPatentUniqueness(It.IsAny<Patent>())).Returns(false); 

            // Act

            bool result = _patentLogic.CheckPatentUniqueness(_defaultPatentItem);

            //Assert

            Assert.IsFalse(result);
        }
    }
}
