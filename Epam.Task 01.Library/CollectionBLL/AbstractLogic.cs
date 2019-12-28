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
    public abstract class AbstractLogic<T> where T: AbstractLibraryItem
    {
        private readonly IAbstractLibraryDAL<AbstractLibraryItem> _librarycollectionDao;
        private IAbstractValidation<T> _absractvalidator;
        public AbstractLogic(IAbstractLibraryDAL<AbstractLibraryItem> librarycollectionDao, IAbstractValidation<T> validator)
        {
            _librarycollectionDao = librarycollectionDao;
            _absractvalidator = validator;
        }
    }
}
