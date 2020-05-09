using System.Collections.Generic;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public int AddNewspaper(Newspaper newspaper)
        {
            return MemoryStorage.AddIssue(newspaper);
        }

        public int EditNewspaper(Newspaper newspaper)
        {
            throw new System.NotImplementedException();
        }

        public Newspaper GetNewspaperById(int id)
        {
            return MemoryStorage.GetNewspaperItemById(id);
        }

        public IEnumerable<Newspaper> GetNewspapers()
        {
            return MemoryStorage.GetAllNewspapers();
        }

        public int SoftDeleteNewspaper(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
