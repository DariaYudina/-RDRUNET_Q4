using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IPatentLogic
    {
        bool AddPatent(out ValidationObject validationObject, Patent patent);

        IEnumerable<Patent> GetPatentItems();
        IEnumerable<Patent> GetPatentsByAuthor(Author author);
    }
}
