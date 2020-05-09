using System.Collections.Generic;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.AbstractDAL.INewspaper
{
    public interface INewspaperDao
    {
        int AddNewspaper(Newspaper newspaper);

        int EditNewspaper(Newspaper newspaper);

        IEnumerable<Newspaper> GetNewspapers();

        Newspaper GetNewspaperById(int id);

        int SoftDeleteNewspaper(int id);
    }
}
