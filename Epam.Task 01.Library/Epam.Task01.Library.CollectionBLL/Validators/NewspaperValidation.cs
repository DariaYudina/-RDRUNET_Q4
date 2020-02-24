using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;

namespace CollectionValidation
{
    public class NewspaperValidation : IIssueValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }

        public bool IsValid { get; set; } = true;

        private ICommonValidation CommonValidation { get; set; }

        private INewspaperValidation IssueValidation { get; set; }

        private const int BottomLineYear = 1400;
        private const int BottomLineCountOfPublishing = 1;

        public NewspaperValidation(ICommonValidation commonValidation, INewspaperValidation issueValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
            IssueValidation = issueValidation;
        }

        public IIssueValidation CheckByCommonValidation(Issue newspaper)
        {
            CommonValidation.CheckPagesCount(newspaper);
            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }

        public IIssueValidation CheckByNewspaperValidation(Issue newspaper)
        {
            IssueValidation.CheckTitle(newspaper.Newspaper).CheckISSN(newspaper.Newspaper).CheckNewspaperCity(newspaper.Newspaper).CheckPublishingCompany(newspaper.Newspaper);
            foreach (var item in IssueValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= IssueValidation.IsValid;
            return this;
        }

        public IIssueValidation CheckCountOfPublishing(Issue newspaper)
        {
            bool notvalid = !CommonValidation.CheckNumericalInRange(newspaper.CountOfPublishing, null, BottomLineCountOfPublishing);
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

        public IIssueValidation CheckDateOfPublishing(Issue newspaper)
        {
            bool notvalid = !CommonValidation.CheckNumericalInRange(newspaper.DateOfPublishing.Year, null, BottomLineYear)
                            || newspaper.DateOfPublishing > DateTime.Now
                            || newspaper.DateOfPublishing.Year != newspaper.YearOfPublishing;

            IsValid &= !notvalid;

            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("DateOfPublishing must be more than 1400 year, less than now date and year of DateOfPublishing must be equal Year", "DateOfPublishing");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public IIssueValidation CheckYearOfPublishing(Issue newspaper)
        {
            bool notvalid = !CommonValidation.CheckNumericalInRange(newspaper.YearOfPublishing, DateTime.Now.Year, BottomLineYear);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("YearOfPublishing must be more than 1400 year and no more then year of now ", "YearOfPublishing");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }
    }
}
