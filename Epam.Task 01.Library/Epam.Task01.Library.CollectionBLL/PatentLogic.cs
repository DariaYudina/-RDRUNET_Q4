using AbstractValidation;
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
        private readonly ICommonValidation _commonValidation;
        private readonly IAuthorValidation _authorValidation;
        public PatentLogic(IPatentDao patentDao, IPatentValidation validator, ICommonValidation commonValidation, IAuthorValidation authorValidation)
        {
            _patentDao = patentDao;
            _patentValidation = validator;
            _commonValidation = commonValidation;
            _authorValidation = authorValidation;

        }
        public bool AddPatent(List<ValidationException> validationResult, Patent patent)
        {
            _commonValidation.ValidationResult = validationResult;
            _patentValidation.ValidationResult = _commonValidation.ValidationResult;
            _authorValidation.ValidationResult = _patentValidation.ValidationResult;
            ICommonValidation commonvalidationObject = _commonValidation.CheckNullReferenceObject(patent).CheckTitle(patent).CheckPagesCount(patent);
            IPatentValidation patentvalidationObject;
            IAuthorValidation authorsvalidationObject = _authorValidation.CheckAuthorsFirstName(patent).CheckAuthorsLastName(patent);
            return false;
        }
        public IEnumerable<Patent> GetPatentItems()
        {
            return _patentDao.GetPatentItems();
        }
    }
}
