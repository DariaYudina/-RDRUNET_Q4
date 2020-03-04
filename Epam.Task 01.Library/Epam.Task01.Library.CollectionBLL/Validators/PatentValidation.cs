using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class PatentValidation : IPatentValidation
    {
        public ValidationObject ValidationObject { get; set; }

        public bool IsValid { get; set; } = true;

        private ICommonValidation CommonValidation { get; set; }

        private const int BottomLineYear = 1474;
        private const string AuthorPattern = @"^((([A-Z][a-z]+)|([A-Z][a-z]+-[A-Z][a-z]+))\s(([a-z]+)\s)?(([A-Z][a-z]+)|((([A-Z][a-z]*)|([a-z]*))(-|')[A-Z][a-z]+))|(([А-Я][а-я]+)|([А-Я][а-я]+-[А-Я][а-я]+))\s(([а-я]+)\s)?(([А-Я][а-я]+)|((([А-Я][а-я]*)|([а-я]*))(-|')[А-Я][а-я]+)))$";
        private const string CountryPattern = @"^(([A-Z][a-z]+)|([А-Я][а-я]+)|([A-Z]+|[А-Я]+))$";
        private const string RegistrationNumberPattern = @"^\d{1,9}$";

        public PatentValidation(ICommonValidation commonValidation)
        {
            ValidationObject = new ValidationObject();
            CommonValidation = commonValidation;
        }

        private void VerificationMethod<T>(Predicate<T> predicateMethod, T checkedValue, string paramsName, string errormassage = "is not valid")
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

        public IPatentValidation CheckApplicationDate(Patent patent)
        {
            if (patent.ApplicationDate == null) return this;
            VerificationMethod(i => i.Year > BottomLineYear || i > DateTime.Now,
                (DateTime)patent?.ApplicationDate,
                nameof(patent.ApplicationDate),
                " must be more than 1474 and less than current year");
            return this;
        }

        public IPatentValidation CheckCountry(Patent patent)
        {
            VerificationMethod(i => !Regex.IsMatch(i, CountryPattern),
            patent.Country,
            nameof(patent.Country));
            return this;
        }

        public IPatentValidation CheckPublicationDate(Patent patent)
        {
            VerificationMethod(
                i => !(i.Year < 1474
                || i > DateTime.Now
                || i < patent.ApplicationDate),
                patent.PublicationDate,
                nameof(patent.PublicationDate),
                "PublicationDate must be more than 1474, less than current year and more than Application date");
            return this;
        }

        public IPatentValidation CheckRegistrationNumber(Patent patent)
        {
            VerificationMethod(i => !Regex.IsMatch(i, RegistrationNumberPattern),
            patent.RegistrationNumber,
            nameof(patent.RegistrationNumber));
            return this;
        }

        public IPatentValidation CheckAuthors(Patent patent)
        {
            string fullname;
            if (patent.Authors != null)
            {
                foreach (Author item in patent.Authors)
                {
                    fullname = item.FirstName + " " + item.LastName;
                    VerificationMethod(i => !Regex.IsMatch(i, AuthorPattern), fullname, nameof(item));
                }
            }
            else
            {
                ValidationException e = new ValidationException($"{nameof(patent.Authors)} must bu not null or empty", nameof(patent.Authors));
                ValidationObject.ValidationExceptions.Add(e);
            }

            return this;
        }

        public IPatentValidation CheckByCommonValidation(Patent patent)
        {
            CommonValidation.CheckTitle(patent).CheckPagesCount(patent);
            foreach (var item in CommonValidation.ValidationObject.ValidationExceptions)
            {
                ValidationObject.ValidationExceptions.Add(item);
            }

            return this;
        }
    }
}
