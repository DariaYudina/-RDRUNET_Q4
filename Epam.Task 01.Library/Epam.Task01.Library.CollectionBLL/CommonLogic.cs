using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionBLL
{
    public class CommonLogic : ICommonLogic
    {
        private readonly ICommonValidation _commonValidator;
        private readonly ICommonDao _commonDao;

        public CommonLogic(ICommonDao commonDao, ICommonValidation validator)
        {
            _commonDao = commonDao;
            _commonValidator = validator;
        }

        public IEnumerable<AbstractLibraryItem> GetAllLibraryItems()
        {
            return _commonDao.GetAllAbstractLibraryItems();
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title)
        {
            return _commonDao.GetLibraryItemsByTitle(title);
        }

        public bool DeleteLibraryItemById(int id)
        {
            return _commonDao.DeleteLibraryItemById(id);
        }

        public IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author)
        {
            return _commonDao.GetBookAndPatentByAuthorId(author.Id);
        }

        public IEnumerable<AbstractLibraryItem> SortByYear()
        {
            return _commonDao.SortByYear();
        }

        public IEnumerable<AbstractLibraryItem> SortByYearDesc()
        {
            return _commonDao.SortByYearDesc();
        }

        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
            return _commonDao.GetLibraryItemsByYearOfPublishing().GroupBy(i => i.YearOfPublishing);
        }

    }
}
