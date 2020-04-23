using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.ViewModels.User
{
    public class SignUpModel : IValidatableObject
    {
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z]$)[A-Za-z][A-Za-z\d-_]+$")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't equal")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            List<string> members = new List<string>();
            if (Password.Contains(Login))
            {
                members.Add("Password");
                members.Add("PasswordConfirm");
                errors.Add(new ValidationResult("Password must not contain Login", members));
            }

            return errors;
        }
    }
}