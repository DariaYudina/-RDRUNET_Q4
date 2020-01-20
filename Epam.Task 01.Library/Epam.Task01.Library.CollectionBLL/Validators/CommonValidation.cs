using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
//"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$" фамилия
//^((([A-Z][a-z]+)((-[a-z]+)?)((-([A-Z][a-z]+))?))|(([А-Я][а-я]+)((-[а-я]+)?)((-([А-Я][а-я]+))?)))$ город (книга)
//^(ISBN\s(([0-7])|(8\d|9[0-4])|(9([5-8]\d)|(9[0-3]))|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X))$ ISBN + добавить метод проверки на 10 цифр
// ^(([A - Z][a - z] +|[А - Я][а - я] +)(\s(([A - Z] |[a - z])[a - z] +) |\s(([А - Я] |[а - я])[а - я] +)) * (-([A - Z][a - z] +) | -([А - Я][а - я] +)) ?)$ город (газета)
// ^(ISSN\s\d{4}-\d{4})$ issn
// ^(([A-Z][a-z]+)|([А-Я][а-я]+)|([A-Z]+|[А-Я]+))$ страна (патент)S
namespace CollectionValidation
{
    public class CommonValidation : ICommonValidation
    {
        public List<ValidationObject> ValidationResult { get; set; }
        public bool IsValid { get; set; } = true;
        public CommonValidation()
        {
            ValidationResult = new List<ValidationObject>();
        }
        public ICommonValidation CheckCommentary(AbstractLibraryItem item)
        {
            if(item == null)
            {
                return this;
            }
            bool notvalid = item.Commentary.Length > 2000;
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
            bool notvalid = item.PagesCount < 0;
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
            bool notvalid = item.Title.Length > 300 | CheckStringIsNullorEmpty(item.Title);
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
        public bool CheckStringIsNullorEmpty(string str)
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
    }
}
