using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL
{
    public class IssueLogic : IIssueLogic
    {
        private readonly IIssueDao _issueDao;
        private readonly IIssueValidation _issueValidation;

        public IssueLogic(IIssueDao issueDao, IIssueValidation issueValidation)
        {
            _issueDao = issueDao;
            _issueValidation = issueValidation;
        }

        public bool AddIssue(List<ValidationObject> validationResult, Issue issue)
        {
            _issueValidation.ValidationResult = validationResult;
            if (issue == null)
            {
                _issueValidation.ValidationResult.Add(new ValidationObject("Issue must be not null and not empty", "Issue"));
                return false;
            }
            IIssueValidation issueValidationObject = _issueValidation.CheckISSN(issue).CheckNewspaperCity(issue).CheckPublishingCompany(issue).CheckTitle(issue);
            if (issueValidationObject.IsValid)
            {
                _issueDao.AddIssue(issue);
                return true;
            }
            return false;
        }

        public IEnumerable<Issue> GetIssueItems()
        {
            return _issueDao.GetIssueItems();
        }

    }
}
