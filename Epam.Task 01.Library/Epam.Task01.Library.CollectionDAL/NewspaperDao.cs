using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public void AddNewspaper(Newspaper item)
        {
            MemoryStorage.AddLibraryItem(item);
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return MemoryStorage.GetLibraryItemByType<Newspaper>(); 
        }
    }
}
