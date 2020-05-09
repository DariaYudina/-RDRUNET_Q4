using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        private readonly IUserValidation _userValidation;

        public UserLogic(IUserDao userDao, IUserValidation validator)
        {
            _userDao = userDao;
            _userValidation = validator;
        }

        public bool AddUser(out ValidationObject validationObject, User user)
        {
            try
            {
                validationObject = _userValidation.ValidationObject;

                if (user == null)
                {
                    _userValidation.ValidationObject.ValidationExceptions.Add
                        (new ValidationException($"{nameof(User)} must be not null and not empty", $"{nameof(User)}"));
                    return false;
                }

                _userValidation.CheckEmptyLogin(user).CheckEmptyPassword(user).CheckLogin(user).CheckPassword(user);
                if (_userValidation.ValidationObject.IsValid)
                {
                    user.Password = ComputeHash(user.Password, new SHA512CryptoServiceProvider());
                    return _userDao.AddUser(user) > 0;
                }

                return false;
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _userDao.GetRoles();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                return _userDao.GetUserById(id);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _userDao.GetUsers();
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            try
            {
                Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

                return BitConverter.ToString(hashedBytes);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool VerifyUser(string login, string password)
        {
            try
            {
                var user = GetUsers().FirstOrDefault(x => string.Equals(x.Login, login, StringComparison.InvariantCulture));

                if (user != null && user.Password == ComputeHash(password, new SHA512CryptoServiceProvider()))
                {
                    return true;
                }
                return false;
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }

        public bool ChangeUserRoles(int userId, List<int> rolesId)
        {
            try
            {
                return _userDao.ChangeUserRoles(userId, rolesId);
            }
            catch (AppLayerException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Logic" };
            }
        }
    }
}
