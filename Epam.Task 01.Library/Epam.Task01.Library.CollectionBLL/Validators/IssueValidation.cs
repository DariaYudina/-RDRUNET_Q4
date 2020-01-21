using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL.Validators
{
    public class IssueValidation : IIssueValidation
    {
        public bool IsValid { get; set; } = true;

        public List<ValidationObject> ValidationResult { get; set; }

        private ICommonValidation CommonValidation { get; set; }

        public IssueValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }

        public IIssueValidation CheckISSN(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("Is nill or white space string", "str");
                        ValidationResult.Add(e);
                    }
                }

                return this;
            }
            else
            {
                return this;
            }
        }

        public IIssueValidation CheckNewspaperCity(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("Is nill or white space string", "str");
                        ValidationResult.Add(e);
                    }
                }

                return this;
            }
            else
            {
                return this;
            }
        }

        public IIssueValidation CheckPublishingCompany(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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
            else
            {
                return this;
            }
        }

        public IIssueValidation CheckYearOfPublishing(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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
            else
            {
                return this;
            }
        }

        public IIssueValidation CheckByCommonValidation(Newspaper newspaper)
        {
            throw new NotImplementedException();
        }
    }
}
