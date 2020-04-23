using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IAuthorLogic
    {
        IEnumerable<Author> GetAuthors();

        bool AddAuthor(out ValidationObject validationObject, Author author);

        Author GetAuthorById(int id);
    }
}
