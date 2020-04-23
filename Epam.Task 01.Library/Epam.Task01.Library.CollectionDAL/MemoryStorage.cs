using System.Collections.Generic;
using System.Linq;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionDAL
{
    internal static class MemoryStorage
    {
        private static readonly Dictionary<int, AbstractLibraryItem> LibraryCatalog;

        private static readonly Dictionary<int, Newspaper> Newspapers;

        static MemoryStorage()
        {
            LibraryCatalog = new Dictionary<int, AbstractLibraryItem>();
            Newspapers = new Dictionary<int, Newspaper>();
        }

        public static int AddLibraryItem(AbstractLibraryItem item)
        {
            int lastid = LibraryCatalog.Any() ? LibraryCatalog.Keys.Max() + 1 : 1;
            item.Id = lastid;
            LibraryCatalog.Add(item.Id, item);
            return item.Id;
        }

        public static IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return LibraryCatalog.Values;
        }

        public static AbstractLibraryItem GetLibraryItemById(int id)
        {
            LibraryCatalog.TryGetValue(id, out AbstractLibraryItem result);
            return result;
        }

        public static bool DeleteLibraryItemById(int id)
        {
            return LibraryCatalog.Remove(id);
        }

        public static bool DeleteIssueById(int id)
        {
            return Newspapers.Remove(id);
        }

        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return LibraryCatalog.Values.OfType<T>().ToList();
        }

        public static int AddIssue(Newspaper issue)
        {
            int lastid = Newspapers.Any() ? Newspapers.Keys.Max() + 1 : 1;
            issue.Id = lastid;
            Newspapers.Add(issue.Id, issue);
            return issue.Id;
        }

        public static IEnumerable<Newspaper> GetAllNewspapers()
        {
            return Newspapers.Values;
        }

        public static Newspaper GetNewspaperItemById(int id)
        {
            Newspapers.TryGetValue(id, out Newspaper issue);
            return issue;
        }
    }
}
