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
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetIssueItems()
        {
            throw new NotImplementedException();
        }
    }
}
