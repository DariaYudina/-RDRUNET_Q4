using System.Collections.Generic;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface ICommonDao
    {
        IEnumerable<AbstractLibraryItem> GetLibraryItems();

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name);

        IEnumerable<AbstractLibraryItem> SortByYear();

        IEnumerable<AbstractLibraryItem> SortByYearDesc();

        bool DeleteLibraryItemById(int id);

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByYearOfPublishing();

        IEnumerable<AbstractLibraryItem> GetBookAndPatentByAuthorId(int id);

        AbstractLibraryItem GetLibraryItemById(int id);
    }
}
