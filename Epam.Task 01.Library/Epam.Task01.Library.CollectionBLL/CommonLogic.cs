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
        public IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
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
        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
            return _commonDao.GetLibraryItemsByYearOfPublishing();
        }

        public IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author)
        {
            var res =  _commonDao.GetAllAbstractLibraryItems().Where(i => i is Patent || i is Book);
            List<AbstractLibraryItem> res2 = new List<AbstractLibraryItem>();
            foreach (var item in res)
            {
                if(item is Book)
                {
                    if (((Book)item).Authors.Contains(author))
                    {
                        res2.Add(item);
                    }

                }
                else
                {
                    if (((Patent)item).Authors.Contains(author))
                    {
                        res2.Add(item);
                    }
                }
            }
            return res2;
        }
        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _commonDao.GetTypeByAuthor<Book>().Where(item => item.Authors.Contains(author));
        }
        public IEnumerable<Patent> GetPatentsByAuthor(Author author)
        {

            return _commonDao.GetTypeByAuthor<Patent>().Where(item => item.Authors.Contains(author));
        }
    }
}
