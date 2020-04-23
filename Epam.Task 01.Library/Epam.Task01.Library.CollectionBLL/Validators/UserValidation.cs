using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL.Validators
{
    public class UserValidation : IUserValidation
    {
        public ValidationObject ValidationObject { get ; set ; }

        public UserValidation()
        {
            ValidationObject = new ValidationObject();
        }

        public IUserValidation CheckLogin(User user)
        {
            VerificationMethod(i => !Regex.IsMatch(i, LoginPattern),
            user.Login,
            nameof(user.Login));
            return this;
        }

        public IUserValidation CheckPassword(User user)
        {
            VerificationMethod(i => i.Contains(user.Login),
            user.Password,
            nameof(user.Password),
            $"{nameof(user.Password)} must not contain Login");
            return this;
        }

        public IUserValidation CheckEmptyLogin(User user)
        {
            VerificationMethod(i => string.IsNullOrEmpty(i),
            user.Login,
            nameof(user.Login),
            $"{nameof(user.Login)} must not null or empty");
            return this;
        }

        public IUserValidation CheckEmptyPassword(User user)
        {
            VerificationMethod(i => string.IsNullOrEmpty(i),
            user.Password,
            nameof(user.Password),
            $"{nameof(user.Password)} must not null or empty");
            return this;
        }

        private const string LoginPattern = @"^(?=.*[A-Za-z]$)[A-Za-z][A-Za-z\d-_]+$";

        private void VerificationMethod<T>(Predicate<T> predicateMethod, T checkedValue, string paramsName, string errormassage = "is not valid")
        {
            try
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
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }
    }
}
