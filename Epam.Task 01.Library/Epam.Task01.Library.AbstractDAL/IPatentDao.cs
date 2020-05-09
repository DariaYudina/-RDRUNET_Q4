using System.Collections.Generic;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IPatentDao
    {
        int AddPatent(Patent item);

        int EditPatent(Patent item);

        IEnumerable<Patent> GetPatents();

        IEnumerable<Patent> GetPatentsByAuthorId(int id);

        int SoftDeletePatent(int id);
    }
}
