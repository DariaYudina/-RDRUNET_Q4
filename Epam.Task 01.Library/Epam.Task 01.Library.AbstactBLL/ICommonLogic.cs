using System.Collections.Generic;
using System.Linq;
using Epam.Task01.Library.Entity;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ICommonLogic
    {
        IEnumerable<AbstractLibraryItem> GetLibraryItems();

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title);

        IEnumerable<AbstractLibraryItem> SortByYear();

        IEnumerable<AbstractLibraryItem> SortByYearDesc();

        bool DeleteLibraryItemById(int id);

        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();

        IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author);

        AbstractLibraryItem GetLibraryItemById(int id);
    }
}
