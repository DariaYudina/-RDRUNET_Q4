using System;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace CollectionValidation
{
    public class IssueValidation : IIssueValidation
    {

        public IssueValidation(ICommonValidation commonValidation, INewspaperValidation newspaperValidation)
        {
            ValidationObject = new ValidationObject();
            CommonValidation = commonValidation;
            NewspaperValidation = newspaperValidation;
        }

        public IIssueValidation CheckByCommonValidation(Issue newspaper)
        {
            CommonValidation.CheckPagesCount(newspaper);
            ValidationObject.ValidationExceptions.AddRange(CommonValidation.ValidationObject.ValidationExceptions);
            return this;
        }

        public IIssueValidation CheckByNewspaperValidation(Issue newspaper)
        {
            NewspaperValidation.CheckTitle(newspaper.Newspaper).CheckISSN(newspaper.Newspaper).CheckNewspaperCity(newspaper.Newspaper).CheckPublishingCompany(newspaper.Newspaper);
            foreach (ValidationException item in NewspaperValidation.ValidationObject.ValidationExceptions)
            {
                ValidationObject.ValidationExceptions.Add(item);
            }

            return this;
        }

        public IIssueValidation CheckCountOfPublishing(Issue newspaper)
        {
            VerificationMethod(i => !(newspaper.CountOfPublishing >= BottomLineCountOfPublishing),
                newspaper.CountOfPublishing,
                nameof(newspaper.CountOfPublishing),
                "CountOfPublishing must be more than 0");
            return this;
        }

        public IIssueValidation CheckDateOfPublishing(Issue newspaper)
        {
            VerificationMethod(i => !(newspaper.DateOfPublishing.Year >= BottomLineYear
                || newspaper.DateOfPublishing > DateTime.Now
                || newspaper.DateOfPublishing.Year != newspaper.YearOfPublishing),
            newspaper.DateOfPublishing,
            nameof(newspaper.DateOfPublishing),
           "DateOfPublishing must be more than 1400 year, less than now date and year of DateOfPublishing must be equal Year");
            return this;
        }

        public IIssueValidation CheckYearOfPublishing(Issue newspaper)
        {
            VerificationMethod(i => !CommonValidation.CheckNumericalInRange(newspaper.YearOfPublishing, DateTime.Now.Year, BottomLineYear),
            newspaper.YearOfPublishing,
            nameof(newspaper.YearOfPublishing),
            "YearOfPublishing must be more than 1400 year and no more then year of now");
            return this;
        }

        public ValidationObject ValidationObject { get; set; }

        private ICommonValidation CommonValidation { get; set; }

        private INewspaperValidation NewspaperValidation { get; set; }

        private const int BottomLineYear = 1400;

        private const int BottomLineCountOfPublishing = 1;

        private void VerificationMethod<T>(Predicate<T> predicateMethod, T checkedValue, string paramsName, string errormassage = "is not valid")
        {
            try
            {
                if (checkedValue != null)
                {
                    if (predicateMethod(checkedValue))
                    {
                        ValidationException e = new ValidationException($"{paramsName} {errormassage}", paramsName);
                        ValidationObject.ValidationExceptions.Add(e);
                    }
                }
                else
                {
                    ValidationException e = new ValidationException($"{paramsName} must bu not null or empty", paramsName);
                    ValidationObject.ValidationExceptions.Add(e);
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }
    }
}
