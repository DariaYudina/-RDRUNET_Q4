using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class AbstractDao<T> : IAbstractLibraryDAL<AbstractLibraryItem>
    {       
        public AbstractDao(){}

        public void AddLibraryItem(AbstractLibraryItem item)
        {
            CommonDao.libraryCatalog.Add(item.LibaryItemId, item);
        }

        public void DeleteLibraryItemById(AbstractLibraryItem item)
        {
            CommonDao.libraryCatalog.Remove(item.LibaryItemId);
        }

        public AbstractLibraryItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, AbstractLibraryItem> GetLibraryItems()
        {
            return CommonDao.libraryCatalog;
        }

        public Dictionary<int, AbstractLibraryItem> GetLibraryItemsByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
