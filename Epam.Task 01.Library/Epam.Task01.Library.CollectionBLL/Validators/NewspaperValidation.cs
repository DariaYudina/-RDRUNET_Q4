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

        private const int BottomLineYear = 1400;
        private const int BottomLineCountOfPublishing = 0;

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
            IssueValidation.CheckTitle(newspaper.Issue).CheckISSN(newspaper.Issue).CheckNewspaperCity(newspaper.Issue).CheckPublishingCompany(newspaper.Issue);
            foreach (var item in IssueValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= IssueValidation.IsValid;
            return this;
        }

        public INewspaperValidation CheckCountOfPublishing(Newspaper newspaper)
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

        public INewspaperValidation CheckDateOfPublishing(Newspaper newspaper)
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

        public INewspaperValidation CheckYearOfPublishing(Newspaper newspaper)
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
