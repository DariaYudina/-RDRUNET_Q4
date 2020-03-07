using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace CollectionValidation
{
    public class BookValidation : IBookValidation
    {

        private ICommonValidation CommonValidation { get; set; }

        private const string BookCityPattern = @"^((([A-Z][a-z]+)((\s([A-Z][a-z]+|[a-z]+))*)((-[a-z]+)?)((-([A-Z][a-z]+))?))|(([А-Я][а-я]+)((\s([А-Я][а-я]+|[а-я]+))*)((-[а-я]+)?)((-([А-Я][а-я]+))?)))$";
        private const string ISBNPattern = @"^(ISBN\s(([0-7])|(8\d|9[0-4])|(9([5-8]\d)|(9[0-3]))|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X))$";
        private const string AuthorPattern = @"^((([A-Z][a-z]+)|([A-Z][a-z]+-[A-Z][a-z]+))\s(([a-z]+)\s)?(([A-Z][a-z]+)|((([A-Z][a-z]*)|([a-z]*))(-|')[A-Z][a-z]+))|(([А-Я][а-я]+)|([А-Я][а-я]+-[А-Я][а-я]+))\s(([а-я]+)\s)?(([А-Я][а-я]+)|((([А-Я][а-я]*)|([а-я]*))(-|')[А-Я][а-я]+)))$";
        private const int BottomLineYear = 1400;
        private const int BottomLineISBNLength = 10;
        private const int UnderLinePublishingCompany = 300;

        public ValidationObject ValidationObject { get; set; }

        public BookValidation(ICommonValidation commonValidation)
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

        public IBookValidation CheckAuthors(Book book)
        {
            string fullname;
            if (book.Authors != null)
            {
                foreach (Author item in book.Authors)
                {
                    fullname = item.FirstName + " " + item.LastName;
                    VerificationMethod(i => !Regex.IsMatch(i, AuthorPattern), fullname, nameof(item));
                }
            }
            else
            {
                ValidationException e = new ValidationException($"{nameof(book.Authors)} must bu not null or empty", nameof(book.Authors));
                ValidationObject.ValidationExceptions.Add(e);
            }

            return this;
        }

        public IBookValidation CheckBookCity(Book book)
        {
            VerificationMethod(i => !Regex.IsMatch(i, BookCityPattern), book.City, nameof(book.City));
            return this;
        }

        public IBookValidation CheckByCommonValidation(Book book)
        {
            CommonValidation.CheckTitle(book).CheckPagesCount(book);
            foreach (var item in CommonValidation.ValidationObject.ValidationExceptions)
            {
                ValidationObject.ValidationExceptions.Add(item);
            }

            return this;
        }

        public IBookValidation CheckISBN(Book book)
        {
            VerificationMethod(i => !Regex.IsMatch(i, ISBNPattern), book.isbn, nameof(book.isbn));
            return this;
        }

        public IBookValidation CheckPublishingCompany(Book book)
        {
            VerificationMethod(
            i => !(i < UnderLinePublishingCompany),
            book.PublishingCompany?.Length,
            nameof(book.PublishingCompany),
            " must be less than 300 characters");
            return this;
        }

        public IBookValidation CheckYearOfPublishing(Book book)
        {
            VerificationMethod(i => !CommonValidation.CheckNumericalInRange(i, DateTime.Now.Year, BottomLineYear),
                book.YearOfPublishing,
                nameof(book.YearOfPublishing),
                " must be more than 1400 and less than current year");
            return this;
        }
    }
}
