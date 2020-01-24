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
        private const string NamePattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
        private const string LastnamePattern = @"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
        private const string CountryPattern = @"^(([A-Z][a-z]+)|([А-Я][а-я]+)|([A-Z]+|[А-Я]+))$";
        private const int BottomLinePageCount = 0;

        public PatentValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }

        public IPatentValidation CheckApplicationDate(Patent patent)
        {
            if (IsValid != false)
            {
                bool notvalid = !CommonValidation.CheckNumericalInRange(patent.ApplicationDate.Year, BottomLineYear, null)
                                 && patent.ApplicationDate > DateTime.Now ;
                IsValid &= !notvalid;
                if (notvalid)
                {
                    if (ValidationResult != null)
                    {
                        ValidationObject e = new ValidationObject("ApplicationDate must be more than 1474 and less than current year", "ApplicationDate");
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

        public IPatentValidation CheckAuthorsFirstName(Patent patent)
        {
            bool notvalid = false;
            foreach (Author item in patent.Authors)
            {
                if (!Regex.IsMatch(item.FirstName, NamePattern))
                {
                    notvalid = true;
                    ValidationObject e = new ValidationObject("Author first name is not valid", "Firstname ");
                    ValidationResult.Add(e);
                    break;
                }
            }

            IsValid &= !notvalid;
            return this;
        }

        public IPatentValidation CheckAuthorsLastName(Patent patent)
        {
            bool notvalid = false;
            foreach (Author item in patent.Authors)
            {
                if (!Regex.IsMatch(item.FirstName, LastnamePattern))
                {
                    notvalid = true;
                    ValidationObject e = new ValidationObject("Author last name is not valid", "Lastname ");
                    ValidationResult.Add(e);
                    break;
                }
            }

            IsValid &= !notvalid;
            return this;
        }

        public IPatentValidation CheckByCommonValidation(Patent patent)
        {
            CommonValidation.CheckTitle(patent).CheckPagesCount(patent);
            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }

        public IPatentValidation CheckCountry(Patent patent)
        {

            bool notvalid = !Regex.IsMatch(patent.Country, CountryPattern);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Country is not valid", "Country");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public IPatentValidation CheckPublicationDate(Patent patent)
        {
            if (IsValid != false)
            {
                bool notvalid = patent.PublicationDate.Year < 1474 && patent.PublicationDate > DateTime.Now && patent.PublicationDate < patent.ApplicationDate;
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
            else
            {
                return this;
            }
        }

        public IPatentValidation CheckRegistrationNumber(Patent patent)
        {
            if (IsValid != false)
            {
                bool notvalid = !CommonValidation.CheckNumericalInRange(patent.PagesCount, null, BottomLinePageCount);
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
    }
}
