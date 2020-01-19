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
        public PatentLogic(IPatentDao patentDao, IPatentValidation validator)
        {
            _patentDao = patentDao;
            _patentValidation = validator;
        }
        public bool AddPatent(List<ValidationObject> validationResult, Patent patent)
        {
            //_commonValidation.ValidationResult = validationResult;
            //_patentValidation.ValidationResult = _commonValidation.ValidationResult;
            //_authorValidation.ValidationResult = _patentValidation.ValidationResult;
            //ICommonValidation commonvalidationObject = _commonValidation.CheckNullReferenceObject(patent).CheckTitle(patent).CheckPagesCount(patent);
            //IPatentValidation patentvalidationObject;
            //IAuthorValidation authorsvalidationObject = _authorValidation.CheckAuthorsFirstName(patent).CheckAuthorsLastName(patent);
            return false;
        }
        public IEnumerable<Patent> GetPatentItems()
        {
            return _patentDao.GetPatentItems();
        }
    }
}
