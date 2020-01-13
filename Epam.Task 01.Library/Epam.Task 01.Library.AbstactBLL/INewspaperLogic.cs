﻿using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface INewspaperLogic
    {
        bool AddNewspaper(List<ValidationException> validationResult, Newspaper newspaper);
        IEnumerable<Newspaper> GetNewspaperItems();
    }
}