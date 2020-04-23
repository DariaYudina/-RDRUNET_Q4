using System.Collections.Generic;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface INewspaperLogic
    {
        IEnumerable<Newspaper> GetNewspapers();

        bool AddNewspaper(out ValidationObject validationObject, Newspaper newspaper);

        Newspaper GetNewspaperById(int id);
    }
}
