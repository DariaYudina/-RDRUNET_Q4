﻿using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IPatentLogic
    {
        bool AddPatent(List<ValidationException> validationResult, Patent patent);
        IEnumerable<Patent> GetPatentItems();
    }
}
