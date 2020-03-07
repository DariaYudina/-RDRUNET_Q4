//using AbstractValidation;
//using CollectionValidation;
//using Epam.Task01.Library.Entity;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UnitTest
//{
//    [TestClass]
//    public class PatentValidationTests
//    {
//        private IPatentValidation _patentValidation;
//        private Patent _defaultPatentItem;
//        private Mock<ICommonValidation> _commonValidationMock;

//        [TestInitialize]
//        public void Initialize()
//        {
//            _commonValidationMock = new Mock<ICommonValidation>();
//            _patentValidation = new PatentValidation(_commonValidationMock.Object);

//            Patent defaultPatentItem = new Patent
//            (authors: new List<Author>() { new Author("", "") },
//              country: "",
//              registrationNumber: "",
//              applicationDate: DateTime.Now,
//              publicationDate: DateTime.Now,
//              title: "",
//              pageCount: 0,
//              commentary: ""
//            );

//            _defaultPatentItem = defaultPatentItem;
//        }

//        [TestMethod]
//        public void CheckApplicationDate_DataNotMoreThanCurrentDataAndMore1474Year_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.ApplicationDate = new DateTime(2000, 1, 1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

//            // Act

//            var validation = _patentValidation.CheckApplicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckApplicationDate_DataLessThan1474_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.ApplicationDate = new DateTime(1473, 1, 1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);
//            // Act

//            var validation = _patentValidation.CheckApplicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckApplicationDate_DataMoreThanCurrentDate_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.ApplicationDate = DateTime.Now.AddDays(1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);
//            // Act

//            var validation = _patentValidation.CheckApplicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckApplicationDate_DataIsNull_ReturnTrue()
//        {
//            // Arrange

//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);
//            // Act

//            _defaultPatentItem.ApplicationDate = null;
//            var validation = _patentValidation.CheckApplicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckRegistrationNumber_RegistrationNumberLengtMore0AndLess10Number_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.RegistrationNumber = "123456789";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckRegistrationNumber(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckRegistrationNumber_RegistrationNumberMore9Number_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.RegistrationNumber = "0123456789";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            var validation = _patentValidation.CheckRegistrationNumber(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckRegistrationNumber_RegistrationNumberIsNull_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.RegistrationNumber = null;
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            var validation = _patentValidation.CheckRegistrationNumber(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryStartWithUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "Country";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryhUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "COUNTRY";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }


//        [TestMethod]
//        public void CheckCountry_CountryStartWithUpperCaseRus_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "Страна";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryhUpperCaserus_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "СТРАНА";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryLessThan200_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "Country";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryRusAndEng_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.Country = "Countryстрана";
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryMoreThan200_ReturnFalse()
//        {
//            // Arrange

//            int inputlength = 201;
//            StringBuilder stringBuilder = new StringBuilder();
//            for (int i = 0; i < inputlength; i++)
//            {
//                stringBuilder.Append("*");
//            }
//            string Text = stringBuilder.ToString();
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            _defaultPatentItem.Country = Text;
//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckCountry_CountryIsNull_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.Country = null;
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            var validation = _patentValidation.CheckCountry(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckPublicationDate_DataNotMoreThanCurrentDataAndMore1474YearAndNotNullAndNotMoreApplicationDate_ReturnTrue()
//        {
//            // Arrange

//            _defaultPatentItem.ApplicationDate = new DateTime(2000, 1, 1);
//            _defaultPatentItem.PublicationDate = _defaultPatentItem.ApplicationDate.Value.AddDays(1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(true);

//            // Act

//            var validation = _patentValidation.CheckPublicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckPublicationDate_PublicationDateLessThanApplicationDate_ReturnFalse()
//        {
//            // Arrange

//            _defaultPatentItem.PublicationDate = new DateTime(1473, 1, 1);
//            _defaultPatentItem.ApplicationDate = _defaultPatentItem.PublicationDate.AddDays(1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);

//            // Act

//            var validation = _patentValidation.CheckPublicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckPublicationDate_DataLessThan1474_ReturnFalse()
//        {
//            // Arrange
//            _defaultPatentItem.ApplicationDate = new DateTime(1470, 1, 1);
//            _defaultPatentItem.PublicationDate = _defaultPatentItem.ApplicationDate.Value.AddDays(1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);
//            // Act

//            var validation = _patentValidation.CheckPublicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckPublicationDate_DataMoreThanCurrentDate_ReturnFalse()
//        {
//            // Arrange
//            _defaultPatentItem.ApplicationDate = DateTime.Now.AddDays(1);
//            _defaultPatentItem.PublicationDate = _defaultPatentItem.ApplicationDate.Value.AddDays(1);
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;
//            _commonValidationMock.Setup(i => i.CheckNumericalInRange(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(false);
//            // Act

//            var validation = _patentValidation.CheckPublicationDate(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameAndLasNameStartWithUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name", "Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }


//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStartWithUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name-Name", "Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenStartWithUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name-Name", "Lastname-Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenAndFamilyPrefixStartWithUpperCaseEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name-Name", "lastname Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStarWithLowerCaseWithFamilyPrefixEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name-Name", "lastname'Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStarWithUpperCaseWithFamilyPrefixEng_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Name-Name", "Lastname'Lastname");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameAndLasNameStartWithUpperCaseRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест", "Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }


//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStartWithUpperCaseRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест-Тест", "Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenStartWithUpperCaseRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест-Тест", "Тест-Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameWithHyphenAndFamilyPrefixStartWithUpperCaseRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест-Тест", "тест Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStarWithLowerCaseWithFamilyPrefixRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест-Тест", "тест'Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameWithHyphenAndLasNameStarWithUpperCaseWithFamilyPrefixRus_ReturnTrue()
//        {
//            // Arrange

//            Author author = new Author("Тест-Тест", "Тест'Тест");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameAndLastNameStartWithLowerCaseeEng_ReturnFalse()
//        {
//            // Arrange

//            Author author = new Author("name", "name");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_NameAndLastNameStartHyphenLowerCaseeEng_ReturnFalse()
//        {
//            // Arrange

//            Author author = new Author("-Name-", "-Lame-");
//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            _defaultPatentItem.Authors[0] = author;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckAuthor_AuthorisNull_ReturnFalse()
//        {
//            // Arrange

//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count + 1;

//            // Act

//            _defaultPatentItem.Authors = null;
//            var validation = _patentValidation.CheckAuthors(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckByCommonValidation_ValidData_ReturnTrue()
//        {
//            // Arrange

//            int expectedValidationResuilCount = _patentValidation.ValidationObject.ValidationExceptions.Count;
//            _commonValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(true);
//            _commonValidationMock.Setup(s => s.ValidationResult).Returns(_patentValidation.ValidationResult);
//            _commonValidationMock.Setup(s => s.CheckTitle(_defaultPatentItem)).Returns(_commonValidationMock.Object);
//            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultPatentItem)).Returns(_commonValidationMock.Object);

//            // Act

//            _defaultPatentItem.Title = "Validtitle";
//            _defaultPatentItem.PagesCount = 10;
//            var validation = _patentValidation.CheckByCommonValidation(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsTrue(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckByCommonValidation_NotValidData_ReturnFalse()
//        {
//            // Arrange

//            _commonValidationMock.Setup(s => s.ValidationObject.IsValid).Returns(false);
//            List<ValidationException> validationObjects = new List<ValidationException>() { new ValidationException("", ""), new ValidationException("", "") };
//            int expectedValidationResuilCount = validationObjects.Count;
//            _commonValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
//            _commonValidationMock.Setup(s => s.CheckTitle(_defaultPatentItem)).Returns(_commonValidationMock.Object);
//            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultPatentItem)).Returns(_commonValidationMock.Object);
//            int inputlength = 301;
//            StringBuilder stringBuilder = new StringBuilder();
//            for (int i = 0; i < inputlength; i++)
//            {
//                stringBuilder.Append("*");
//            }
//            string Text = stringBuilder.ToString();
//            // Act

//            _defaultPatentItem.Title = Text;
//            _defaultPatentItem.PagesCount = 300;
//            var validation = _patentValidation.CheckByCommonValidation(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }

//        [TestMethod]
//        public void CheckByCommonValidation_TitleIsNull_ReturnFalse()
//        {
//            // Arrange

//            _commonValidationMock.Setup(s => s.ValidationObject.ValidationObject.IsValid).Returns(false);
//            List<ValidationException> validationObjects = new List<ValidationException>() { new ValidationException("", ""), new ValidationException("", "") };
//            int expectedValidationResuilCount = validationObjects.Count;
//            _commonValidationMock.Setup(s => s.ValidationResult).Returns(validationObjects);
//            _commonValidationMock.Setup(s => s.CheckTitle(_defaultPatentItem)).Returns(_commonValidationMock.Object);
//            _commonValidationMock.Setup(s => s.CheckPagesCount(_defaultPatentItem)).Returns(_commonValidationMock.Object);

//            // Act

//            _defaultPatentItem.Title = null;
//            _defaultPatentItem.PagesCount = 300;
//            var validation = _patentValidation.CheckByCommonValidation(_defaultPatentItem);
//            bool result = validation.ValidationObject.IsValid;
//            int actualValidationResuilCount = validation.ValidationObject.ValidationExceptions.Count;

//            //Assert

//            Assert.IsFalse(result);
//            Assert.AreEqual(expectedValidationResuilCount, actualValidationResuilCount);
//        }
//    }
//}
