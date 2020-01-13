using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface ICommonDao
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems();
        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name);
        bool DeleteLibraryItemById(AbstractLibraryItem item);
        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();
    }
}
