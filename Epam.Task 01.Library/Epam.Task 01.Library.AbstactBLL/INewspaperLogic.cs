using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Epam.Task_01.Library.AbstactBLL
{
    public interface INewspaperLogic
    {
        IEnumerable<Newspaper> GetNewspaperItems();
        bool AddNewspaper(out ValidationObject validationObject, Newspaper newspaper );
        Newspaper GetNewspaperItemById(int id);
    }
}
