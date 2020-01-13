using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ISearchLogic
    {
        IEnumerable<Book> GetBooksByAuthor(Author author);
        IEnumerable<Patent> GetPatentsByAuthor(Author author);
        IEnumerable<IWithAuthorProperty> GetBooksAndPatentsByAuthor(Author author);
    }
}
