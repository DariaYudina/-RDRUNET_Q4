using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;

namespace Epam.Task01.Library.CollectionBLL
{
    public class IssueLogic : IIssueLogic
    {
        private readonly IIssueDao _issueDao;
        private readonly IIssueValidation _issueValidation;

        public IssueLogic(IIssueDao issueDao, IIssueValidation validator)
        {
            _issueDao = issueDao;
            _issueValidation = validator;
        }

        public bool AddIssue(out ValidationObject validationObject, Issue issue)
        {
            validationObject = _issueValidation.ValidationObject;

            if (issue == null)
            {
                _issueValidation.ValidationObject.ValidationExceptions.Add(new ValidationException("Issue must be not null and not empty", "Newspaper"));
                return false;
            }

            if(issue.Newspaper == null)
            {
                _issueValidation.ValidationObject.ValidationExceptions.Add(new ValidationException("Object reference not set to an instance of an object", "Issue"));
                return false;
            }

            IIssueValidation newspapervalidationObject = _issueValidation
                .CheckByCommonValidation(issue)
                .CheckByNewspaperValidation(issue)
                .CheckCountOfPublishing(issue)
                .CheckYearOfPublishing(issue)
                .CheckDateOfPublishing(issue);

            if (newspapervalidationObject.ValidationObject.IsValid)
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