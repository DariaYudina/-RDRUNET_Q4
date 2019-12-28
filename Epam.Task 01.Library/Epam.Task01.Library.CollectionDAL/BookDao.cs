using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class BookDao : IBookDAL<Book>
    {
        public void AddLibraryItem(AbstractLibraryItem item)
        {
            throw new NotImplementedException();
        }

        public void DeleteLibraryItemById(AbstractLibraryItem item)
        {
            throw new NotImplementedException();
        }

        public AbstractLibraryItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, AbstractLibraryItem> GetLibraryItems()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, AbstractLibraryItem> GetLibraryItemsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
