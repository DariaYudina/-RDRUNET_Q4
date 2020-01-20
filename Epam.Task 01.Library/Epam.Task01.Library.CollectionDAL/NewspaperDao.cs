using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.CollectionDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public void AddNewspaper(Newspaper item)
        {
            MemoryStorage.AddLibraryItem(item);
        }

        public bool CheckNewspaperUniqueness(Newspaper newspaper)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return MemoryStorage.GetLibraryItemByType<Newspaper>();
        }
    }
}
