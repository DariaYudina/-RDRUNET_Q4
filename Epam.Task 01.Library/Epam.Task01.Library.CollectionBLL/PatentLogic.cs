using System;
using System.Collections.Generic;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionBLL
{
    public class PatentLogic : IPatentLogic
    {
        private readonly IPatentDao _patentDao;
        private readonly IPatentValidation _patentValidation;

        public PatentLogic(IPatentDao patentDao, IPatentValidation validator)
        {
            _patentDao = patentDao;
            _patentValidation = validator;
        }

        public bool AddPatent(out ValidationObject validationObject, Patent patent)
        {
            try
            {
                validationObject = _patentValidation.ValidationObject;

                if (patent == null)
                {
                    _patentValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(patent)} must be not null and not empty", nameof(patent)));
                    return false;
                }

                if (patent.Authors == null || patent.Authors.Count == 0)
                {
                    _patentValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(patent.Authors)} list must be not null and not empty",
                        nameof(patent.Authors)));
                    return false;
                }

                _patentValidation
                .CheckByCommonValidation(patent)
                .CheckCountry(patent)
                .CheckRegistrationNumber(patent)
                .CheckPublicationDate(patent);
                //.CheckAuthors(patent);

                if (_patentValidation.ValidationObject.IsValid)
                {
                    return _patentDao.AddPatent(patent) > 0;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool EditPatent(out ValidationObject validationObject, Patent patent)
        {
            try
            {
                validationObject = _patentValidation.ValidationObject;

                if (patent == null)
                {
                    _patentValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(patent)} must be not null and not empty", nameof(patent)));
                    return false;
                }

                if (patent.Authors == null || patent.Authors.Count == 0)
                {
                    _patentValidation.ValidationObject.ValidationExceptions.Add(new ValidationException($"{nameof(patent.Authors)} list must be not null and not empty",
                        nameof(patent.Authors)));
                    return false;
                }

                _patentValidation
                .CheckByCommonValidation(patent)
                .CheckCountry(patent)
                .CheckRegistrationNumber(patent)
                .CheckPublicationDate(patent);

                if (_patentValidation.ValidationObject.IsValid)
                {
                    return _patentDao.EditPatent(patent) >= 0;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Patent> GetPatents()
        {
            try
            {
                return _patentDao.GetPatents();
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Patent> GetPatentsByAuthor(Author author)
        {
            try
            {
                return _patentDao.GetPatentsByAuthorId(author.Id);
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}
