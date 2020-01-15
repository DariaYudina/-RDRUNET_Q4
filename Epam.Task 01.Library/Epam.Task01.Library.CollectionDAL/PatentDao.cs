using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.CollectionDAL
{
    public class PatentDao : IPatentDao
    {
        public void AddLibraryItem(Patent item)
        {
            MemoryStorage.AddLibraryItem(item);
        }
        public IEnumerable<Patent> GetPatentItems()
        {
            return MemoryStorage.GetLibraryItemByType<Patent>();
        }
    }
}
