using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IPatentDao
    {
        int AddPatent(Patent item);

        IEnumerable<Patent> GetPatentItems();

        IEnumerable<Patent> GetPatentsByAuthorId(int id);

    }
}
