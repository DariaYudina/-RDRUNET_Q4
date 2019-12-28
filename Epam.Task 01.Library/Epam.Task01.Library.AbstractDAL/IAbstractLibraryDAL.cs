using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IAbstractLibraryDAL<T> where T : AbstractLibraryItem
    {
        void AddLibraryItem(AbstractLibraryItem item);
        void DeleteLibraryItemById(AbstractLibraryItem item);
        Dictionary<int, AbstractLibraryItem> GetLibraryItems();
        Dictionary<int, AbstractLibraryItem> GetLibraryItemsByName(string name);
        AbstractLibraryItem GetItemById(int id);
    }
}
