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
            throw new NotImplementedException();
        }
        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return _newspaperDao.GetNewspaperItems();
        }
    }
}
