using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool DeleteLibraryItemById(AbstractLibraryItem item)
        {
            return libraryCatalog.Remove(item.LibaryItemId);
        }
        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return libraryCatalog.OfType<T>();
        }
    }
}
