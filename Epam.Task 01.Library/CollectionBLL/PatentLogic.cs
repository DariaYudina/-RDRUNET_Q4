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
    class PatentLogic : AbstractLogic<Patent>, IPatentLogic
    {
        public PatentLogic(IAbstractLibraryDAL<AbstractLibraryItem> librarycollectionDao, IAbstractValidation<Patent> validator) 
            : base(librarycollectionDao, validator)
        {
        }
    }
}
