using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    internal static class MemoryStorage
    {
        private static readonly Dictionary<int, AbstractLibraryItem> _libraryCatalog;
        private static readonly Dictionary<int, Newspaper> _newspapers;

        static MemoryStorage()
        {
            _libraryCatalog = new Dictionary<int, AbstractLibraryItem>();
            _newspapers = new Dictionary<int, Newspaper>();
        }

        public static int AddLibraryItem(AbstractLibraryItem item)
        {
            var lastid =_libraryCatalog.Any() ? _libraryCatalog.Keys.Max()+1 : 1;
            item.Id = lastid;
            _libraryCatalog.Add(item.Id, item);
            return item.Id;
        }

        public static IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return _libraryCatalog.Values;
        }

        public static AbstractLibraryItem GetLibraryItemById(int id)
        {
            AbstractLibraryItem result;
            _libraryCatalog.TryGetValue(id, out result); 
            return result;
        }

        public static bool DeleteLibraryItemById(int id)
        {
            return _libraryCatalog.Remove(id);
        }

        public static bool DeleteIssueById(int id)
        {
            return _newspapers.Remove(id);
        }

        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return _libraryCatalog.Values.OfType<T>().ToList();
        }

        public static int AddIssue(Newspaper issue)
        {
            var lastid = _newspapers.Any() ? _newspapers.Keys.Max() + 1 : 1;
            issue.Id = lastid;
            _newspapers.Add(issue.Id, issue);
            return issue.Id;
        }

        public static IEnumerable<Newspaper> GetAllNewspapers()
        {
            return _newspapers.Values;
        }

        public static Newspaper GetNewspaperItemById(int id)
        {
            _newspapers.TryGetValue(id, out Newspaper issue);
            return issue;
        }
    }
}
