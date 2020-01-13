using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ICommonLogic
    {
        IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems();
        IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title);
        bool DeleteLibraryItemById(int id);
        IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing();
    }
}
