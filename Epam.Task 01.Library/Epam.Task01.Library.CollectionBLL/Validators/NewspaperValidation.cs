using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace CollectionValidation
{
    public class NewspaperValidation : INewspaperValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }

        public bool IsValid { get; set; } = true;
        private ICommonValidation CommonValidation { get; set; }
        public NewspaperValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }
        public INewspaperValidation CheckISSN(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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

        public INewspaperValidation CheckNewspaperCity(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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

        public INewspaperValidation CheckPublishingCompany(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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

        public INewspaperValidation CheckYearOfPublishing(Newspaper newspaper)
        {
            if (IsValid != false)
            {
                bool notvalid = newspaper.PagesCount < 0;
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

        public INewspaperValidation CheckByCommonValidation(Newspaper newspaper)
        {
            CommonValidation.CheckTitle(newspaper).CheckPagesCount(newspaper);
            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }
    }
}
