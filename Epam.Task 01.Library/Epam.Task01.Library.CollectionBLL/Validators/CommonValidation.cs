using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class CommonValidation : ICommonValidation
    {
        private const int UnderlineCommentaryLength = 2000;
        private const int BottomlinePagesCountLength = 0;
        private const int UnderlineTitle = 300;

        public ValidationObject ValidationObject { get; set; }

        public CommonValidation()
        {
             ValidationObject = new ValidationObject();
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

        public ICommonValidation CheckCommentary(AbstractLibraryItem item)
        {
            VerificationMethod(i => !(i< UnderlineCommentaryLength),
            item.Commentary?.Length,
            nameof(item.Commentary),
            "must be less than 2000 characters");
            return this;
        }

        public ICommonValidation CheckPagesCount(AbstractLibraryItem item)
        {
            VerificationMethod(i => !(i > BottomlinePagesCountLength),
            item.PagesCount,
            nameof(item.PagesCount),
            "must be more then 0");
            return this;
        }

        public ICommonValidation CheckTitle(AbstractLibraryItem item)
        {
            if (!string.IsNullOrEmpty(item.Title))
            {
                VerificationMethod(i => !(i < UnderlineTitle),
                item.Title?.Length,
                nameof(item.Title),
                "must be less than 300 characters and must be not null or empty");
            }
            else
            {
                ValidationException e = new ValidationException($"{nameof(item.Title)} must bu not null or empty", nameof(item.Title));
                ValidationObject.ValidationExceptions.Add(e);
            }

            return this;
        }

        public bool CheckNumericalInRange(int number, int bottomline, int underline)
        {
            if ( number >= bottomline && number <= underline)
            {
                return true;
            }

            return false;
        }
    }
}
