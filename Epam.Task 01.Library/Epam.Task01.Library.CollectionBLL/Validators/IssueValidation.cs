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

        public IssueValidation()
        {
            ValidationResult = new List<ValidationObject>();
        }

        public IIssueValidation CheckISSN(Issue issue)
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

            return this;
        }

        public IIssueValidation CheckNewspaperCity(Issue issue)
        {
            string NewspaperCityPattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)(\s(([A-Z]|[a-z])[a-z]+)|\s(([А-Я]|[а-я])[а-я]+))*(-([A-Z][a-z]+)|-([А-Я][а-я]+))?)$";
            bool notvalid = !Regex.IsMatch(issue.Issn, NewspaperCityPattern);
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
        public IIssueValidation CheckPublishingCompany(Issue issue)
        {
            bool notvalid = issue.;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Is null or white space string", "str");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }
        public IIssueValidation CheckYearOfPublishing(Issue issue)
        {
            bool notvalid = ;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Is null or white space string", "str");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public IIssueValidation CheckTitle(Issue issue)
        {
            bool notvalid = ;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Is null or white space string", "str");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }
    }
}
