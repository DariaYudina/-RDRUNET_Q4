using System;
using System.Collections.Generic;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;

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
            try
            {
                validationObject = _issueValidation.ValidationObject;

                if (issue == null)
                {
                    _issueValidation.ValidationObject.ValidationExceptions.Add
                        (new ValidationException($"{nameof(Issue)} must be not null and not empty", $"{nameof(Issue)}"));
                    return false;
                }

                    _issueValidation
                    //.CheckByCommonValidation(issue)
                    //.CheckByNewspaperValidation(issue)
                    //.CheckCountOfPublishing(issue)
                    //.CheckYearOfPublishing(issue)
                    .CheckDateOfPublishing(issue);

                    if (_issueValidation.ValidationObject.IsValid)
                    {
                        return _issueDao.AddIssue(issue) > 0;
                    }

                    return false;
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool EditIssue(out ValidationObject validationObject, Issue issue)
        {
            try
            {
                validationObject = _issueValidation.ValidationObject;

                if (issue == null)
                {
                    _issueValidation.ValidationObject.ValidationExceptions.Add
                        (new ValidationException($"{nameof(Issue)} must be not null and not empty", $"{nameof(Issue)}"));
                    return false;
                }

                _issueValidation
                .CheckDateOfPublishing(issue);

                if (_issueValidation.ValidationObject.IsValid)
                {
                    return _issueDao.EditIssue(issue) >= 0;
                }

                return false;
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Issue> GetIssues()
        {
            try
            {
                return _issueDao.GetIssues();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId, int currentId)
        {
            try
            {
                return _issueDao.GetIssuesByNewspaperId(newspaperId, currentId);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool SoftDeleteIssue(int id)
        {
            try
            {
                return _issueDao.SoftDeleteIssue(id) > 0;
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}