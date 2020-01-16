using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class PatentValidation : IPatentValidation
    {
        public List<ValidationException> ValidationResult { get; set; }
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
                        ValidationException e = new ValidationException("ApplicationDate must be more than 1474 and less than current year", "ApplicationDate");
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

        public IPatentValidation CheckCountry(Patent patent)
        {
            string countryPattern = @"^(([A - Z][a - z] +) | ([А - Я][а - я] +) | ([A - Z] +|[А - Я] +))$";
            bool notvalid = !Regex.IsMatch(patent.Country, countryPattern);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationException e = new ValidationException("Country is not valid", "Country");
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
                        ValidationException e = new ValidationException("PublicationDate must be more than 1474, less than current year and more than Application date", "PublicationDate");
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
                        ValidationException e = new ValidationException("Is nill or white space string", "str");
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
