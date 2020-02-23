using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL.Validators
{
    public class IssueValidation : IIssueValidation
    {
        public bool IsValid { get; set; } = true;

        public List<ValidationObject> ValidationResult { get; set; }

        private ICommonValidation CommonValidation { get; set; }

        private const int TimberLinePublishingCompany = 300;
        private const int TimberLineTitle = 300;

        public IssueValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }

        public IIssueValidation CheckISSN(Newspaper issue)
        {
            if (issue.Issn != null)
            {
                string IssnPattern = @"^(ISSN\s\d{4}-\d{4})$";
                bool notvalid = !Regex.IsMatch(issue.Issn, IssnPattern);
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("Issn is not valid", "Issn");
                        ValidationResult.Add(e);
                    }
                }
            }
            return this;
        }

        public IIssueValidation CheckNewspaperCity(Newspaper issue)
        {
            string NewspaperCityPattern = @"^((([A-Z][a-z]+)(\s(([A-Z]|[a-z])[a-z]+))*(-([A-Z][a-z]+))?)|(([А-Я][а-я]+)(\s(([А-Я]|[а-я])[а-я]+))*(-([А-Я][а-я]+))?))$";
            bool notvalid = !Regex.IsMatch(issue.City, NewspaperCityPattern);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("NewspaperCity is not valid", "NewspaperCity");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public IIssueValidation CheckPublishingCompany(Newspaper issue)
        {
            if (issue.PublishingCompany != null)
            {
                bool notvalid = !CommonValidation.CheckNumericalInRange(issue.PublishingCompany.Length, TimberLinePublishingCompany, null);
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("PublishingCompany must be less than 300 characters", "PublishingCompany");
                        ValidationResult.Add(e);
                    }
                }
            }
            else
            {
                IsValid &= false;
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("PublishingCompany must be not null or empty", "PublishingCompany");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public IIssueValidation CheckTitle(Newspaper issue)
        {
            if (issue.Title != null)
            {
                bool notvalid = !CommonValidation.CheckNumericalInRange(issue.Title.Length, TimberLineTitle, null) || CheckStringIsNullorEmpty(issue.Title);
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("Title must be less than 300 characters", "Title");
                        ValidationResult.Add(e);
                    }
                }
            }
            else
            {
                IsValid &= false;
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Title must not null or empty", "Title");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public bool CheckStringIsNullorEmpty(string str)
        {
            bool notvalid = string.IsNullOrWhiteSpace(str);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Is null or white space string", "str");
                    ValidationResult.Add(e);
                }

                return true;
            }

            return false;
        }
    }
}
