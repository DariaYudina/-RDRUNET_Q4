using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class MemoryStorage
    {
        private static readonly Dictionary<int, AbstractLibraryItem> libraryCatalog;
        static MemoryStorage()
        {
            libraryCatalog = new Dictionary<int, AbstractLibraryItem>();
        }
        public static void AddLibraryItem(AbstractLibraryItem item)
        {
            var lastid = MemoryStorage.libraryCatalog.Any() ? libraryCatalog.Keys.Max()+1 : 1;
            item.LibaryItemId = lastid;
            libraryCatalog.Add(item.LibaryItemId, item);
        }
        public static IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return libraryCatalog.Values;
        }
        public static bool GetLibraryItemById(int id, out AbstractLibraryItem result)
        {
            return libraryCatalog.TryGetValue(id, out result);
        }
        public static bool DeleteLibraryItemById(int id)
        {
            return libraryCatalog.Remove(id);
        }
        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return libraryCatalog.OfType<T>();
        }
    }
}
