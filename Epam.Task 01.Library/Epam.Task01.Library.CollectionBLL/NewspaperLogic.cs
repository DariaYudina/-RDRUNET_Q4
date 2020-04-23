using System;
using System.Collections.Generic;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.Entity;

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
            try
            {
                validationObject = _newspaperValidation.ValidationObject;
                if (newspaper == null)
                {
                    _newspaperValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(newspaper)} must be not null and not empty", nameof(newspaper)));
                    return false;
                }

                _newspaperValidation.CheckISSN(newspaper)
                                    .CheckNewspaperCity(newspaper)
                                    .CheckPublishingCompany(newspaper)
                                    .CheckTitle(newspaper);
                if (_newspaperValidation.ValidationObject.IsValid)
                {
                    return _newspaperDao.AddNewspaper(newspaper) > 0;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public Newspaper GetNewspaperById(int id)
        {
            try
            {
                return _newspaperDao.GetNewspaperById(id);
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Newspaper> GetNewspapers()
        {
            try
            {
                return _newspaperDao.GetNewspapers();
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}
