using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;

namespace CollectionValidation
{
    public class NewspaperValidation : INewspaperValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }

        public bool IsValid { get; set; } = true;

        private ICommonValidation CommonValidation { get; set; }
        private IIssueValidation IssueValidation { get; set; }

        public NewspaperValidation(ICommonValidation commonValidation, IIssueValidation issueValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
            IssueValidation = issueValidation;
        }

        public INewspaperValidation CheckByCommonValidation(Newspaper newspaper)
        {
            CommonValidation.CheckPagesCount(newspaper);
            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }

        public INewspaperValidation CheckByIssueValidation(Newspaper newspaper)
        {
            IssueValidation.CheckByCommonValidation(newspaper).CheckISSN(newspaper).CheckNewspaperCity(newspaper).CheckPublishingCompany(newspaper).CheckYearOfPublishing(newspaper);
            foreach (var item in IssueValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= IssueValidation.IsValid;
            return this;
        }

        public INewspaperValidation CheckCountOfPublishing(Newspaper newspaper)
        {
            bool notvalid = newspaper.CountOfPublishing <= 0;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("CountOfPublishing must be more than 0", "CountOfPublishing");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }

        public INewspaperValidation CheckDateOfPublishing(Newspaper newspaper)
        {
            bool notvalid = newspaper.DateOfPublishing.Year < 1400 || newspaper.DateOfPublishing > DateTime.Now || newspaper.DateOfPublishing.Year != newspaper.YearOfPublishing;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("DateOfPublishing must be more than 1400, less than now date and year of DateOfPublishing must be equal Year", "DateOfPublishing");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }
    }
}
