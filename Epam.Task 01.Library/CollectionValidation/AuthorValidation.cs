using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CollectionValidation
{
    public class AuthorValidation : IAuthorValidation
    {
        public List<ValidationException> ValidationResult { get; set; }
        public bool IsValid { get; set; } = true;
        public IAuthorValidation CheckAuthorsFirstName(IWithAuthorProperty withAuthor)
        {
                bool notvalid = false;
                string namePattern = @"^(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]+-[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
                foreach (var item in withAuthor.Authors)
                {
                    if (!Regex.IsMatch(item.FirstName, namePattern))
                    {
                        notvalid = true;
                        ValidationException e = new ValidationException("Author first name is not valid", "Firstname ");
                        ValidationResult.Add(e);
                        break;
                    }
                }
                IsValid &= !notvalid;
                return this;
        }
        public IAuthorValidation CheckAuthorsLastName(IWithAuthorProperty withAuthor)
        {
                bool notvalid = false;
                string lastnamePattern = @"^(([a-z]+)\s)?(([A-Z][a-z]+|[А-Я][а-я]+)|([A-Z][a-z]*(-|')[A-Z][a-z]+|[А-Я][а-я]+-[А-Я][а-я]+))$";
                foreach (var item in withAuthor.Authors)
                {
                    if (!Regex.IsMatch(item.FirstName, lastnamePattern))
                    {
                        notvalid = true;
                        ValidationException e = new ValidationException("Author last name is not valid", "Lastname ");
                        ValidationResult.Add(e);
                        break;
                    }
                }
                IsValid &= !notvalid;
                return this;
        }
    }
}
