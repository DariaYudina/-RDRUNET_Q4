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

        public IPatentValidation CheckApplicationDate(Patent patent)
        {
            if (IsValid != false)
            {
                bool notvalid = patent.ApplicationDate.Year > 1474 && patent.ApplicationDate < DateTime.Now ;
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
            string namePattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
            foreach (Author item in patent.Authors)
            {
                if (!Regex.IsMatch(item.FirstName, namePattern))
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
            string lastnamePattern = @"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
            foreach (Author item in patent.Authors)
            {
                if (!Regex.IsMatch(item.FirstName, lastnamePattern))
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

        public IPatentValidation CheckCountry(Patent patent)
        {
            string countryPattern = @"^(([A - Z][a - z] +) | ([А - Я][а - я] +) | ([A - Z] +|[А - Я] +))$";
            bool notvalid = !Regex.IsMatch(patent.Country, countryPattern);
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
                bool notvalid = patent.PublicationDate.Year > 1474 && patent.PublicationDate < DateTime.Now && patent.PublicationDate > patent.ApplicationDate;
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
                bool notvalid = patent.PagesCount < 0;
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
