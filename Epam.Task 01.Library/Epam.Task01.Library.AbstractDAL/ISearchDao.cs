using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface ISearchDao
    {
        IEnumerable<T> GetTypeByAuthor<T>() where T : IWithAuthorProperty;
    }
}
