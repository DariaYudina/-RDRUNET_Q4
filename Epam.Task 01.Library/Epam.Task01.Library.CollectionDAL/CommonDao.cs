using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class CommonDao : ICommonDao
    {
        public bool DeleteLibraryItemById(AbstractLibraryItem item)
        {
            return MemoryStorage.DeleteLibraryItemById(item);
        }

        public IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return MemoryStorage.GetAllAbstractLibraryItems();
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name)
        {
            return MemoryStorage.GetAllAbstractLibraryItems().Where(i => i.Title == name);
        }
        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
            return MemoryStorage.GetAllAbstractLibraryItems().GroupBy(item => item.YearOfPublishing);
        }
    }
}
