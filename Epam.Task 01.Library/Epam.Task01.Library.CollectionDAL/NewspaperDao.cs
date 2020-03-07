using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public int AddNewspaper(Newspaper newspaper)
        {
            return MemoryStorage.AddIssue(newspaper);
        }

        public Newspaper GetNewspaperItemById(int id)
        {
            return MemoryStorage.GetNewspaperItemById(id);
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return MemoryStorage.GetAllNewspapers();
        }
    }
}
