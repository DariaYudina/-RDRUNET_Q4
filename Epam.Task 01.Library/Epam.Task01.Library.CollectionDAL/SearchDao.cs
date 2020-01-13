using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    class SearchDao : ISearchDao
    {
        public IEnumerable<T> GetTypeByAuthor<T>()
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<T>();
        }
    }
}
