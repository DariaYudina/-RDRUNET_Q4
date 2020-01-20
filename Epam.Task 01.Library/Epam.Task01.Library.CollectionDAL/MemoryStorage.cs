﻿using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class MemoryStorage
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
            item.LibaryItemId = lastid;
            _libraryCatalog.Add(item.LibaryItemId, item);
        }

        public static IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return _libraryCatalog.Values;
        }

        public static bool GetLibraryItemById(int id, out AbstractLibraryItem result)
        {
            return _libraryCatalog.TryGetValue(id, out result);
        }

        public static bool DeleteLibraryItemById(int id)
        {
            return _libraryCatalog.Remove(id);
        }

        public static IEnumerable<T> GetLibraryItemByType<T>()
        {
            return _libraryCatalog.OfType<T>();
        }

        public static IEnumerable<AbstractLibraryItem> Test()
        {
            return _libraryCatalog.Values.Where(i => i is Patent || i is Book);
        }

        public static void AddIssue(Issue issue)
        {
            var lastid = MemoryStorage._issues.Any() ? _issues.Keys.Max() + 1 : 1;
            issue.IssueId = lastid;
            _issues.Add(issue.IssueId, issue);
        }
        public static IEnumerable<Issue> GetAllIssues()
        {
            return _issues.Values;
        }
    }
}
