using System;
using System.Text.RegularExpressions;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionBLL.Validators
{
    public class NewspaperValidation : INewspaperValidation
    {
        public ValidationObject ValidationObject { get; set; }

        public NewspaperValidation(ICommonValidation commonValidation)
        {
            ValidationObject = new ValidationObject();
            CommonValidation = commonValidation;
        }

        public INewspaperValidation CheckISSN(Newspaper issue)
        {
            VerificationMethod(i => !Regex.IsMatch(i, IssnPattern), issue.Issn, nameof(issue.Issn));
            return this;
        }

        public INewspaperValidation CheckNewspaperCity(Newspaper issue)
        {
            VerificationMethod(i => !Regex.IsMatch(i, NewspaperCityPattern), issue.City, nameof(issue.City));
            return this;
        }

        public INewspaperValidation CheckPublishingCompany(Newspaper issue)
        {
            VerificationMethod(i => !(i.Length < UnderLinePublishingCompany),
                issue.PublishingCompany,
                nameof(issue.PublishingCompany),
                " must be less than 300 characters");
            return this;
        }

        public INewspaperValidation CheckTitle(Newspaper issue)
        {
            if (issue.Title != null)
            {
                if (!string.IsNullOrEmpty(issue.Title))
                {
                    VerificationMethod(i => !(i.Length < UnderLineTitle),
                    issue.Title,
                    nameof(issue.Title),
                    " must be less than 300 characters");
                }

                return this;
            }

            return this;
        }

        private ICommonValidation CommonValidation { get; set; }
        
        private const int UnderLinePublishingCompany = 300;
        
        private const int UnderLineTitle = 300;
       
        private const string IssnPattern = @"^(ISSN\s\d{4}-\d{4})$";
        
        private const string NewspaperCityPattern = @"^((([A-Z][a-z]+)(\s(([A-Z]|[a-z])[a-z]+))*(-([A-Z][a-z]+))?)|(([А-Я][а-я]+)(\s(([А-Я]|[а-я])[а-я]+))*(-([А-Я][а-я]+))?))$";

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
