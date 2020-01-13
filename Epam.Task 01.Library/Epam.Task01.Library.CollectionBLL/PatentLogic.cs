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
    public class PatentLogic : IPatentLogic 
    {
        private IPatentDao _patentDao;
        private IPatentValidation _patentValidation;
        public PatentLogic(IPatentDao patentDao, IPatentValidation validator)
        {
            _patentDao = patentDao;
            _patentValidation = validator;
        }
        public bool AddPatent(List<ValidationException> validationResult, Patent patent)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Patent> GetPatentItems()
        {
           return _patentDao.GetPatentItems();
        }
    }
}
