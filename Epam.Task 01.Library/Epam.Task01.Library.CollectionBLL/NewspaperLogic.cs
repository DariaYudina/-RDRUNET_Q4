using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;

namespace Epam.Task01.Library.CollectionBLL
{
    public class NewspaperLogic : INewspaperLogic
    {
        private readonly INewspaperDao _newspaperDao;
        private readonly INewspaperValidation _newspaperValidation;

        public NewspaperLogic(INewspaperDao newspaperDao, INewspaperValidation validator)
        {
            _newspaperDao = newspaperDao;
            _newspaperValidation = validator;
        }

        public bool AddNewspaper(List<ValidationObject> validationResult, Newspaper newspaper)
        {
            _newspaperValidation.ValidationResult = validationResult;
            if (newspaper == null)
            {
                _newspaperValidation.ValidationResult.Add(new ValidationObject("Object reference not set to an instance of an object", "Book"));
                return false;
            }

            INewspaperValidation newspapervalidationObject = _newspaperValidation.CheckByCommonValidation(newspaper);
            if (!CheckNewspaperUniqueness(newspaper))
            {
                _newspaperValidation.ValidationResult.Add(new ValidationObject("Newspaper is not unique ", "Newspaper"));
                return false;
            }

            if (newspapervalidationObject.IsValid)
            {
                _newspaperDao.AddNewspaper(newspaper);
                return true;
            }

            return false;
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return _newspaperDao.GetNewspaperItems();
        }

        public bool CheckNewspaperUniqueness(Newspaper newspaper)
        {
            return _newspaperDao.CheckNewspaperUniqueness(newspaper);
        }
    }
}