using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class BookValidation : IBookValidation
    {
        public bool IsValid { get; set; } = true;
        public List<ValidationObject> ValidationResult { get; set; }
        private ICommonValidation CommonValidation { get; set; }
        public BookValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }
        public IBookValidation CheckBookCity(Book book)
        {
            string bookCityPattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
            bool notvalid = !Regex.IsMatch(book.City, bookCityPattern);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Book city is not valid", "City");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }
        public IBookValidation CheckISBN(Book book)
        {
            if (book.isbn == null)
            {
                return this;
            }
            string ISBNPattern = @"^(ISBN\s(([0-7])|(8\d|9[0-4])|(9([5-8]\d)|(9[0-3]))|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X))$";
            bool notvalid = !Regex.IsMatch(book.isbn, ISBNPattern);
            if (!notvalid)
            {
                notvalid &= CheckISBNLength(book.isbn);
            }
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("ISBN is not valid", "ISBN");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }
        public IBookValidation CheckPublishingCompany(Book book)
        {
            bool notvalid = book.PublishingCompany.Length > 300;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("PublishingCompany must be less than 300 characters", "PublishingCompany");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }
        public IBookValidation CheckYearOfPublishing(Book book)
        {
            bool notvalid = book.YearOfPublishing < 1400 && book.YearOfPublishing > DateTime.Now.Year;
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("YearOfPublishing must be more than 1400 and less than current year", "YearOfPublishing");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }

        public IBookValidation CheckAuthorsFirstName(Book book)
        {
            bool notvalid = false;
            string namePattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
            foreach (Author item in book.Authors)
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
        public IBookValidation CheckAuthorsLastName(Book book)
        {
            bool notvalid = false;
            string lastnamePattern = @"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
            foreach (Author item in book.Authors)
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
        public IBookValidation CheckByCommonValidation(Book book)
        {
            CommonValidation.CheckTitle(book).CheckPagesCount(book);
            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }
            this.IsValid &= CommonValidation.IsValid;
            return this;
        }
        private bool CheckISBNLength(string isbn)
        {
            string wishoutISBN = isbn.Substring(5, isbn.Length - 5);
            string withoutdefice = wishoutISBN.Replace("-", "");
            return withoutdefice.Length != 10;
        }
    }
}
