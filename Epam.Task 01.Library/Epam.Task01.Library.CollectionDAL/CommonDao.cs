using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class CommonDao : ICommonDao
    {
        public bool DeleteLibraryItemById(int id)
        {
            return MemoryStorage.DeleteLibraryItemById(id);
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
            //return MemoryStorage.GetAllAbstractLibraryItems().GroupBy(item => item.Title);
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetTypeByAuthor<T>() where T : AbstractLibraryItem
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<T>();
        }

        public IEnumerable<T> GetTypesByAuthor<T, G>()
            where T : AbstractLibraryItem
            where G : AbstractLibraryItem
        {
            //return MemoryStorage.GetAllAbstractLibraryItems().Where(i => i is T || i is G);
            throw new System.NotImplementedException();
        }

    }
}
