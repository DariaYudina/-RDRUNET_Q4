using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface INewspaperDao
    {
        void AddNewspaper(Newspaper item);
        IEnumerable<Newspaper> GetNewspaperItems();
    }
}
