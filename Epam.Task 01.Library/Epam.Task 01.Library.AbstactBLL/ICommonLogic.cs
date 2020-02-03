using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ICommonLogic
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems(); // ты получаешь абстрактные сущности?

        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title);  // а здесь нет?

        IEnumerable<AbstractLibraryItem> SortByYear();

        IEnumerable<AbstractLibraryItem> SortByYearDesc();

        bool DeleteLibraryItemById(int id);

        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();

        IEnumerable<AbstractLibraryItem> GetBooksAndPatentsByAuthor(Author author);

        IEnumerable<Book> GetBooksByAuthor(Author author);

        IEnumerable<Patent> GetPatentsByAuthor(Author author);
    }
}
