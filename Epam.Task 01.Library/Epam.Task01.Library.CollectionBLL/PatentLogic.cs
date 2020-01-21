﻿using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;

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

        public bool AddPatent(List<ValidationObject> validationResult, Patent patent)
        {
            _patentValidation.ValidationResult = validationResult;
            if (patent == null)
            {
                _patentValidation.ValidationResult.Add(new ValidationObject("Object reference not set to an instance of an object", "Book"));
                return false;
            }
            if (patent.Authors == null || patent.Authors.Count == 0)
            {
                _patentValidation.ValidationResult.Add(new ValidationObject("Authors must be not null and not empty", "Authors"));
                return false;
            }
            IPatentValidation patentvalidationObject = _patentValidation.CheckByCommonValidation(patent).CheckCountry(patent).CheckRegistrationNumber(patent).CheckPublicationDate(patent).CheckAuthorsFirstName(patent).CheckAuthorsLastName(patent);
            if (!CheckPatentUniqueness(patent))
            {
                _patentValidation.ValidationResult.Add(new ValidationObject("Patent is not unique ", "Patent"));
                return false;
            }

            if (patentvalidationObject.IsValid)
            {
                _patentDao.AddPatent(patent);
                return true;
            }

            return false;
        }

        public IEnumerable<Patent> GetPatentItems()
        {
            return _patentDao.GetPatentItems();
        }

        public bool CheckPatentUniqueness(Patent patent)
        {
            return _patentDao.CheckPatentUniqueness(patent);
        }
    }
}
