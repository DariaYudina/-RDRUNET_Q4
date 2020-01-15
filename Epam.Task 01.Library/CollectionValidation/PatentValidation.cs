using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

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

        public IPatentValidation CheckCountry(Patent patent)
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

        public IPatentValidation CheckPublicationDate(Patent patent)
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
