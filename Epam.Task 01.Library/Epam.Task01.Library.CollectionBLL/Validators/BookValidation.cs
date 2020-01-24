﻿using AbstractValidation;
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

        private const string BookCityPattern = @"^((([A-Z][a-z]+)((-[a-z]+)?)((-([A-Z][a-z]+))?))|(([А-Я][а-я]+)((-[а-я]+)?)((-([А-Я][а-я]+))?)))$";
        private const string ISBNPattern = @"^(ISBN\s(([0-7])|(8\d|9[0-4])|(9([5-8]\d)|(9[0-3]))|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X))$";
        private const string NamePattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
        private const string LastnamePattern = @"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
        private const int BottomLineYear = 1400;
        private const int TimberLineISBNLength = 10;
        private const int TimberLinePublishingCompany = 300;

        public BookValidation(ICommonValidation commonValidation)
        {
            ValidationResult = new List<ValidationObject>();
            CommonValidation = commonValidation;
        }

        public IBookValidation CheckBookCity(Book book)
        {
            bool notvalid = !Regex.IsMatch(book.City, BookCityPattern);
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
            bool notvalid = !CommonValidation.CheckNumericalInRange(book.PublishingCompany.Length, TimberLinePublishingCompany, null);
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
            bool notvalid = !CommonValidation.CheckNumericalInRange(book.YearOfPublishing, DateTime.Now.Year, BottomLineYear);
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
            foreach (Author item in book.Authors)
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

        public IBookValidation CheckAuthorsLastName(Book book)
        {
            bool notvalid = false;
            foreach (Author item in book.Authors)
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

        public IBookValidation CheckByCommonValidation(Book book)
        {
            CommonValidation.CheckTitle(book).CheckPagesCount(book);

            foreach (var item in CommonValidation.ValidationResult)
            {
                this.ValidationResult.Add(item);
            }

            IsValid &= CommonValidation.IsValid;
            return this;
        }

        private bool CheckISBNLength(string isbn)
        {
            string wishoutISBN = isbn.Substring(5, isbn.Length - 5);
            string withoutdefice = wishoutISBN.Replace("-", "");
            return withoutdefice.Length != TimberLineISBNLength;
        }
    }
}
