using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public interface IAuthorValidation
    {
        ValidationObject ValidationObject { get; set; }

        IAuthorValidation CheckAuthor(Author author);
    }
}
