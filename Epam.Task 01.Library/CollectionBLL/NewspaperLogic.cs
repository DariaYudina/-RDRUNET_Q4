using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionBLL
{
    class NewspaperLogic : AbstractLogic<Newspaper>, INewspaperLogic
    {
        public NewspaperLogic(IAbstractLibraryDAL<AbstractLibraryItem> librarycollectionDao, IAbstractValidation<Newspaper> validator) 
            : base(librarycollectionDao, validator)
        {
        }
    }
}
