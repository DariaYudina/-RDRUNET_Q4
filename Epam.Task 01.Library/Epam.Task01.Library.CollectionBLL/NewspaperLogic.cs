using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL
{
    public class NewspaperLogic : INewspaperLogic
    {
        private INewspaperDao _newspaperDao;
        private INewspaperValidation _newspaperValidation;
        public NewspaperLogic(INewspaperDao newspaperDao, INewspaperValidation validator)
        {
            _newspaperDao = newspaperDao;
            _newspaperValidation = validator;
        }

        public bool AddNewspaper(List<ValidationException> validationResult, Newspaper newspaper)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Newspaper> GetNewspaperItems()
        {
           return _newspaperDao.GetNewspaperItems();
        }
    }
}
