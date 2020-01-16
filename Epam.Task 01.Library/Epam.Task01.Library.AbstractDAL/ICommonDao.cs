using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface ICommonDao
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems();
        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name);
        bool DeleteLibraryItemById(int id);
        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();
        IEnumerable<T> GetTypeByAuthor<T>() where T : AbstractLibraryItem;
        IEnumerable<T> GetTypesByAuthor<T, G>() where T : AbstractLibraryItem where G : AbstractLibraryItem;
    }
}
