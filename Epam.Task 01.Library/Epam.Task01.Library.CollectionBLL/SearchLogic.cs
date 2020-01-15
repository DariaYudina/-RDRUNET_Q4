using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionBLL
{
    public class SearchLogic : ISearchLogic
    {
        private readonly ISearchDao _searchDao;
        public SearchLogic(ISearchDao searchDao)
        {
            _searchDao = searchDao;
        }
        public IEnumerable<IWithAuthorProperty> GetBooksAndPatentsByAuthor(Author author)
        {
            return _searchDao.GetTypeByAuthor<IWithAuthorProperty>().Where(item => item.Authors.Contains(author));
        }
        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _searchDao.GetTypeByAuthor<Book>().Where(item => item.Authors.Contains(author));
        }
        public IEnumerable<Patent> GetPatentsByAuthor(Author author)
        {
            return _searchDao.GetTypeByAuthor<Patent>().Where(item => item.Authors.Contains(author));
        }
    }
}
