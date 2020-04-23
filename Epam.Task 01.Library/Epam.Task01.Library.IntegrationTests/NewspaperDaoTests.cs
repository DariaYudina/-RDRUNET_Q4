using System.Configuration;
using System.Linq;
using System.Transactions;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epam.Task01.Library.IntegrationTests
{
    [TestClass]
    public class NewspaperDaoTests
    {
        private Newspaper _defaultNewspaperItem;
        private INewspaperDao _newspaperDao;
        private ICommonDao _commonDao;
        private TransactionScope scope;
        private SqlConnectionConfig sqlConnectionConfig;

        [TestInitialize]
        public void Initialize()
        {
            sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"]
                .ConnectionString);
            _newspaperDao = new NewspaperDBDao(sqlConnectionConfig);
            _commonDao = new CommonDBDao(sqlConnectionConfig);

            Newspaper defaultNewspaperItem = new Newspaper
            (
              title: "",
              city: "",
              publishingCompany: "",
              issn: ""
            );

            _defaultNewspaperItem = defaultNewspaperItem;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void AddIssue_AddingValidItem_Successfully()
        {
            //Arrange
            int expectedCount = _newspaperDao.GetNewspapers().Count() + 1;

            //Act
            _newspaperDao.AddNewspaper(_defaultNewspaperItem);
            int actualValidationResuilCount = _newspaperDao.GetNewspapers().Count();

            //Assert
            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            _commonDao.DeleteLibraryItemById(_defaultNewspaperItem.Id);
        }

        [TestMethod]
        public void GetIssueById_FoundExistingId_ReturnIssue()
        {
            // Arrange
            int expectedCount = _newspaperDao.GetNewspapers().Count() + 1;

            // Act
            _newspaperDao.AddNewspaper(_defaultNewspaperItem);
            int actualValidationResuilCount = _newspaperDao.GetNewspapers().Count();

            //Assert
            Assert.AreEqual(expectedCount, actualValidationResuilCount);
            _commonDao.DeleteLibraryItemById(_defaultNewspaperItem.Id);
        }

        [TestMethod]
        public void GetIssueById_FoundNotExistingI_ReturnNull()
        {
            // Act
            Newspaper item = _newspaperDao.GetNewspaperById(_defaultNewspaperItem.Id);

            //Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public void GetIssueItems_ToNotEmptyDao_ReturnItems()
        {
            // Arrange
            _newspaperDao.AddNewspaper(_defaultNewspaperItem);
            int expectedCount = 1;

            // Act
            int result = _newspaperDao.GetNewspapers().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
            _commonDao.DeleteLibraryItemById(_defaultNewspaperItem.Id);
        }

        public void GetIssueItems_ToEmptyDao_ReturnEmptyCollection()
        {
            // Arrange
            int expectedCount = 0;

            // Act
            int result = _newspaperDao.GetNewspapers().Count();

            //Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
