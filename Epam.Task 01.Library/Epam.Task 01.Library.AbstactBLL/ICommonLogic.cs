using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ICommonLogic
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems();

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title);

        IEnumerable<AbstractLibraryItem> SortByYear();

        IEnumerable<AbstractLibraryItem> SortByYearDesc();

        bool DeleteLibraryItemById(int id);

        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();

        IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author);

        IEnumerable<Book> GetBooksByAuthor(Author author);

        IEnumerable<Patent> GetPatentsByAuthor(Author author);
    }
}
