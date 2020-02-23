﻿using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public interface IIssueValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        IIssueValidation CheckNewspaperCity(Newspaper issue);
        IIssueValidation CheckPublishingCompany(Newspaper issue);
        IIssueValidation CheckISSN(Newspaper issue);
        IIssueValidation CheckTitle(Newspaper issue);
        bool CheckStringIsNullorEmpty(string str);
    }
}
