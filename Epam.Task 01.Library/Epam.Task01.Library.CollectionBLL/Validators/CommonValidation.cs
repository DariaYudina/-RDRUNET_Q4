using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionValidation
{
    public class CommonValidation : ICommonValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }

        private const int TimberlineCommentaryLength = 2000;
        private const int BottomlinePagesCountLength = 0;
        private const int TimberlineTitle = 300;

        public bool IsValid { get; set; } = true;

        public CommonValidation()
        {
            ValidationResult = new List<ValidationObject>();
        }

        public ICommonValidation CheckCommentary(AbstractLibraryItem item)
        {
            if (item == null)
            {
                return this;
            }

            bool notvalid = !CheckNumericalInRange(item.Commentary.Length, TimberlineCommentaryLength, null);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Commentary", "Commentary must be less than 2000 characters");
                    ValidationResult.Add(e);
                }
            }

            return this;
        }

        public ICommonValidation CheckPagesCount(AbstractLibraryItem item)
        {
            bool notvalid = !CheckNumericalInRange(item.PagesCount, null, BottomlinePagesCountLength);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("PagesCount must be more then 0", "PagesCount");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }

        public ICommonValidation CheckTitle(AbstractLibraryItem item)
        {
            bool notvalid = !CheckNumericalInRange(item.Title.Length, TimberlineTitle, null) | CheckStringIsNullorEmpty(item.Title);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Title must be less than 300 characters", "Title");
                    ValidationResult.Add(e);
                }
            }
            return this;
        }

        private bool CheckStringIsNullorEmpty(string str)
        {
            bool notvalid = string.IsNullOrWhiteSpace(str);
            IsValid &= !notvalid;
            if (notvalid)
            {
                if (ValidationResult != null)
                {
                    ValidationObject e = new ValidationObject("Is nill or white space string", "str");
                    ValidationResult.Add(e);
                }
                return true;
            }
            return false;
        }

        public bool CheckNumericalInRange(int number, int? timberline, int? bottomline )
        {
            if (bottomline == null)
            {
                if (number <= timberline)
                {
                    return true;
                }
            }

            if (timberline == null)
            {
                if (number <= bottomline)
                {
                    return true;
                }
            }

            if ( number <= timberline && number >= bottomline)
            {
                return true;
            }

            if (timberline == null && bottomline == null)
            {
                return true;
            }

            return false;
        }
    }
}
