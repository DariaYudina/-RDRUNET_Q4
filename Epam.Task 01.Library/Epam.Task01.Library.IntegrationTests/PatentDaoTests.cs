using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.IntegrationTests
{
    [TestClass]
    public class PatentDaoTests
    {
        private Patent _defaultPatentItem;
        private IPatentDao _patentDao;

        [TestInitialize]
        public void Initialize()
        {
            _patentDao = new PatentDao();

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
    }
}
