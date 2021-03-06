using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionBLL
{
    public class AuthorLogic : IAuthorLogic
    {
        private readonly IAuthorDao _authorDao;
        private readonly IAuthorValidation _authorValidation;

        public AuthorLogic(IAuthorDao authorDao, IAuthorValidation authorValidation)
        {
            _authorDao = authorDao;
            _authorValidation = authorValidation;
        }
        public bool AddAuthor(out ValidationObject validationObject, Author author)
        {
            try
            {
                validationObject = _authorValidation.ValidationObject;
                if (author == null)
                {
                    validationObject.ValidationExceptions.Add(new ValidationException($"{nameof(author)} must be not null and not empty", nameof(author)));
                    return false;
                }
                _authorValidation.CheckAuthor(author);
                if (_authorValidation.ValidationObject.IsValid)
                {
                    return _authorDao.AddAuthor(author) > 0;
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

        public bool EditAuthor(out ValidationObject validationObject, Author author)
        {
            try
            {
                validationObject = _authorValidation.ValidationObject;
                if (author == null)
                {
                    validationObject.ValidationExceptions.Add(new ValidationException($"{nameof(author)} must be not null and not empty", nameof(author)));
                    return false;
                }
                _authorValidation.CheckAuthor(author);
                if (_authorValidation.ValidationObject.IsValid)
                {
                    return _authorDao.EditAuthor(author) > 0;
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

        public Author GetAuthorById(int id)
        {
            try
            {
                return _authorDao.GetAuthorById(id);
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

        public IEnumerable<Author> GetAuthors()
        {
            try
            {
                return _authorDao.GetAuthors();
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

        public IEnumerable<Author> GetAuthorsByString(string search)
        {
            try
            {
                if (search.Length > 0 && search.Length < 251)
                {
                    return _authorDao.GetAuthorsByString(search);
                }
                else
                {
                    return null;
                }
                
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
