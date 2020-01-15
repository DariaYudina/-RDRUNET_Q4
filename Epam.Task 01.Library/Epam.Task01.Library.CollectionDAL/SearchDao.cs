using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class SearchDao : ISearchDao
    {
        public IEnumerable<T> GetTypeByAuthor<T>() where T : IWithAuthorProperty
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<T>();
        }
    }
}
