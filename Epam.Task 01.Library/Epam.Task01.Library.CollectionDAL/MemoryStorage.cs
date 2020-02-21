using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    internal static class MemoryStorage
    {
        private static readonly Dictionary<int, AbstractLibraryItem> _libraryCatalog;
        private static readonly Dictionary<int, Issue> _issues;

        static MemoryStorage()
        {
            _libraryCatalog = new Dictionary<int, AbstractLibraryItem>();
            _issues = new Dictionary<int, Issue>();
        }

        public static void AddLibraryItem(AbstractLibraryItem item)
        {
            var lastid = MemoryStorage._libraryCatalog.Any() ? _libraryCatalog.Keys.Max()+1 : 1;
            item.Id = lastid;
            _libraryCatalog.Add(item.Id, item);
        }

        public static IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return _libraryCatalog.Values;
        }

        public static bool GetLibraryItemById(int id, out AbstractLibraryItem result)
        {
            return _libraryCatalog.TryGetValue(id, out result); //зачем возвращать bool если можно вернуть сущность?
        }

        public static bool DeleteLibraryItemById(int id)
        {
            return _libraryCatalog.Remove(id);
        }

        public static bool DeleteIssueById(int id)
        {
            return _issues.Remove(id);
        }

        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return _libraryCatalog.Values.OfType<T>().ToList();
        }

        public static void AddIssue(Issue issue)
        {
            var lastid = MemoryStorage._issues.Any() ? _issues.Keys.Max() + 1 : 1;
            issue.Id = lastid;
            _issues.Add(issue.Id, issue);
        }

        public static IEnumerable<Issue> GetAllIssues()
        {
            return _issues.Values;
        }

        public static Issue GetIssueItemById(int id)
        {
            _issues.TryGetValue(id, out Issue issue);
            return issue;
        }
    }
}
