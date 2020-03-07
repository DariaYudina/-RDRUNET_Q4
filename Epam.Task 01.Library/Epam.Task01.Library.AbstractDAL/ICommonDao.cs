using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface ICommonDao
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems();

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name);

        IEnumerable<AbstractLibraryItem> SortByYear();

        IEnumerable<AbstractLibraryItem> SortByYearDesc();

        bool DeleteLibraryItemById(int id);

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByYearOfPublishing();

        IEnumerable<AbstractLibraryItem> GetBookAndPatentByAuthorId(int id);

    }
}
