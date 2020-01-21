using AbstractValidation;
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
        IIssueValidation CheckNewspaperCity(Issue issue);
        IIssueValidation CheckPublishingCompany(Issue issue);
        IIssueValidation CheckISSN(Issue issue);
        IIssueValidation CheckTitle(Issue issue);
    }
}
