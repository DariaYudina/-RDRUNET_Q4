﻿using AbstractValidation;
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
    public class CommonLogic :ICommonLogic
    {
        private ICommonValidation _commonValidator;
        private ICommonDao _commonDao;
        public CommonLogic(ICommonDao commonDao, ICommonValidation validator)
        {
            _commonDao = commonDao;
            _commonValidator = validator;
        }
        public IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            return _commonDao.GetAllAbstractLibraryItems();
        }
        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string title)
        {
            return _commonDao.GetLibraryItemsByTitle(title);
        }
        public bool DeleteLibraryItemById(int id)
        {
            return _commonDao.DeleteLibraryItemById(id);
        }
        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
           return _commonDao.GetLibraryItemsByYearOfPublishing();
        }
    }
}