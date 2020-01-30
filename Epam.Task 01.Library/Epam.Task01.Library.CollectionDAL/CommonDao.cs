﻿using Epam.Task01.Library.AbstractDAL;
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
            return MemoryStorage.GetAllAbstractLibraryItems().GroupBy(i => i.YearOfPublishing);
        }

        public IEnumerable<T> GetTypeByAuthor<T>() where T : AbstractLibraryItem
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<T>();
        }

        public IEnumerable<AbstractLibraryItem> GetTwoTypesByAuthor<T, G>()
            where T : AbstractLibraryItem
            where G : AbstractLibraryItem
        {
            return MemoryStorage.GetAllAbstractLibraryItems().Where(i => i is T || i is G);
        }

        public IEnumerable<AbstractLibraryItem> SortByYear()
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OrderBy(u => u.YearOfPublishing);
        }

        public IEnumerable<AbstractLibraryItem> SortByYearDesc()
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OrderByDescending(u => u.YearOfPublishing);
        }

        public void AddAbstractLibraryItem(AbstractLibraryItem item)
        {
            MemoryStorage.AddLibraryItem(item);
        }

        public bool DeleteIssueItemById(int id)
        {
            return MemoryStorage.DeleteIssueById(id);
        }
    }
}
