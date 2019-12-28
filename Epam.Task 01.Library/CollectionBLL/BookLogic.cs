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
    class BookLogic : AbstractLogic<Book>, IBookLogic
    {
        public BookLogic(IAbstractLibraryDAL<AbstractLibraryItem> librarycollectionDao, IAbstractValidation<Book> validator) 
               : base(librarycollectionDao, validator)
        {
        }
    }
}
