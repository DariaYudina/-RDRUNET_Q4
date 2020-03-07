using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
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
        private readonly INewspaperDao _newspaperDao;
        private readonly INewspaperValidation _newspaperValidation;

        public NewspaperLogic(INewspaperDao newspaperDao, INewspaperValidation newspaperValidation)
        {
            _newspaperDao = newspaperDao;
            _newspaperValidation = newspaperValidation;
        }

        public bool AddNewspaper(out ValidationObject validationObject, Newspaper newspaper)
        {
            validationObject = _newspaperValidation.ValidationObject;
            if (newspaper == null)
            {
                _newspaperValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(newspaper)} must be not null and not empty", nameof(newspaper)));
                return false;
            }
            INewspaperValidation issueValidationObject = _newspaperValidation.CheckISSN(newspaper)
                                                                         .CheckNewspaperCity(newspaper)
                                                                         .CheckPublishingCompany(newspaper)
                                                                         .CheckTitle(newspaper);
            if (_newspaperValidation.ValidationObject.IsValid)
            {
                return _newspaperDao.AddNewspaper(newspaper) > 0;
            }

            return false;
        }

        public Newspaper GetNewspaperItemById(int id)
        {
            return _newspaperDao.GetNewspaperItemById(id);
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return _newspaperDao.GetNewspaperItems();
        }
    }
}
