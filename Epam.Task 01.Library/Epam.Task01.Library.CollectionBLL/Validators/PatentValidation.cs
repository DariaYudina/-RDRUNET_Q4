using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class PatentValidation : IPatentValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }

        public bool IsValid { get; set; } = true;

        private ICommonValidation CommonValidation { get; set; }

        private const int BottomLineYear = 1474;
        private const string AuthorPattern = @"^((([A-Z][a-z]+)|([A-Z][a-z]+-[A-Z][a-z]+))\s(([a-z]+)\s)?(([A-Z][a-z]+)|((([A-Z][a-z]*)|([a-z]*))(-|')[A-Z][a-z]+))|(([А-Я][а-я]+)|([А-Я][а-я]+-[А-Я][а-я]+))\s(([а-я]+)\s)?(([А-Я][а-я]+)|((([А-Я][а-я]*)|([а-я]*))(-|')[А-Я][а-я]+)))$";
        private const string CountryPattern = @"^(([A-Z][a-z]+)|([А-Я][а-я]+)|([A-Z]+|[А-Я]+))$";
        private const string RegistrationNumberPattern = @"^\d{1,9}$";

        public PatentValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }

        public IPatentValidation CheckApplicationDate(Patent patent)
        {
            if (patent.ApplicationDate != null)
            {
                var data = (DateTime)patent.ApplicationDate;
                bool notvalid = !CommonValidation.CheckNumericalInRange(data.Year, BottomLineYear, null) || patent.ApplicationDate > DateTime.Now;
                IsValid &= !notvalid;
                if (notvalid)       // ты можешь сама глядя на свой код сказать, что тут будет?
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("ApplicationDate must be more than 1474 and less than current year", "ApplicationDate");
                        ValidationResult.Add(e);
                    }
                }
            }

            return this;
        }

        public IPatentValidation CheckCountry(Patent patent)
        {
            bool notvalid = false;
            if (patent.Country != null)
            {
                notvalid = !Regex.IsMatch(patent.Country, CountryPattern);
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("Country is not valid", "Country");
                        ValidationResult.Add(e);
                    }
                }
            }
            else
            {
                notvalid = true;
                ValidationObject e = new ValidationObject("Country must be not null or empty", "Country");
                ValidationResult.Add(e);
            }

            IsValid &= !notvalid;
            return this;
        }

        public IPatentValidation CheckPublicationDate(Patent patent)
        {
            bool notvalid = patent.PublicationDate.Year < 1474 || patent.PublicationDate > DateTime.Now || patent.PublicationDate < patent.ApplicationDate;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("PublicationDate must be more than 1474, less than current year and more than Application date", "PublicationDate");
                    ValidationResult.Add(e);
                }
            }

            return this;

        }

        public IPatentValidation CheckRegistrationNumber(Patent patent)
        {
            bool notvalid = false;
            if (patent.RegistrationNumber != null)
            {
                notvalid = !Regex.IsMatch(patent.RegistrationNumber, RegistrationNumberPattern);
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("RegistrationNumber is not valid", "RegistrationNumber");
                        ValidationResult.Add(e);
                    }
                }
            }
            else
            {
                notvalid = true;
                ValidationObject e = new ValidationObject("RegistrationNumber must be not null or empty", "RegistrationNumber");
                ValidationResult.Add(e);
            }

            IsValid &= !notvalid;
            return this;
        }

        public IPatentValidation CheckAuthors(Patent patent)
        {
            bool notvalid = false;
            string fullname;
            if (patent.Authors != null)
            {
                foreach (Author item in patent.Authors)
                {
                    fullname = item.FirstName + " " + item.LastName;
                    if (!Regex.IsMatch(fullname, AuthorPattern))
                    {
                        notvalid = true;
                        ValidationObject e = new ValidationObject("Author full name is not valid", "Author");
                        ValidationResult.Add(e);
                        break;
                    }
                }
            }
            else
            {
                notvalid = true;
                ValidationObject e = new ValidationObject("Author must be not null or empty", "Author");
                ValidationResult.Add(e);
            }

            IsValid &= !notvalid;
            return this;
        }

        public IPatentValidation CheckByCommonValidation(Patent patent)
        {
            CommonValidation.CheckTitle(patent).CheckPagesCount(patent);
            foreach (var item in CommonValidation.ValidationResult)
            {
                ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }
    }
}
