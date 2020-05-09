using System;
using System.Collections.Generic;
using System.Linq;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionBLL
{
    public class CommonLogic : ICommonLogic
    {
        private readonly ICommonDao _commonDao;

        public CommonLogic(ICommonDao commonDao)
        {
            _commonDao = commonDao;
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItems()
        {
            try
            {
                return _commonDao.GetLibraryItems();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title)
        {
            try
            {
                return _commonDao.GetLibraryItemsByTitle(title);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool DeleteLibraryItemById(int id)
        {
            try
            {
                return _commonDao.DeleteLibraryItemById(id);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author)
        {
            try
            {
                return _commonDao.GetBookAndPatentByAuthorId(author.Id);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<AbstractLibraryItem> SortByYear()
        {
            try
            {
                return _commonDao.SortByYear();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<AbstractLibraryItem> SortByYearDesc()
        {
            try
            {
                return _commonDao.SortByYearDesc();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
            try
            {
                return _commonDao.GetLibraryItemsByYearOfPublishing().GroupBy(i => i.YearOfPublishing);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public AbstractLibraryItem GetLibraryItemById(int id)
        {
            try
            {
                return _commonDao.GetLibraryItemById(id);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}
