using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface ISearchLogic
    {
        IEnumerable<Book> GetBooksByAuthor(Author author);
        IEnumerable<Patent> GetPatentsByAuthor(Author author);
        IEnumerable<IWithAuthorProperty> GetBooksAndPatentsByAuthor(Author author);
    }
}
