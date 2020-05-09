using System.Collections.Generic;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IPatentLogic
    {
        bool AddPatent(out ValidationObject validationObject, Patent patent);

        bool EditPatent(out ValidationObject validationObject, Patent patent);

        IEnumerable<Patent> GetPatents();

        IEnumerable<Patent> GetPatentsByAuthor(Author author);

        bool SoftDeletePatent(int id);
    }
}
