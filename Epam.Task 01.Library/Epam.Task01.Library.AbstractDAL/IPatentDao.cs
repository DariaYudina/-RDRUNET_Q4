using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IPatentDao
    {
        void AddLibraryItem(Patent item);
        IEnumerable<Patent> GetPatentItems();
    }
}
