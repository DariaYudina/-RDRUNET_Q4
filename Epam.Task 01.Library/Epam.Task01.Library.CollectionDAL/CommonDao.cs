using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class CommonDao 
    {
        public static readonly Dictionary<int, AbstractLibraryItem> libraryCatalog = new Dictionary<int, AbstractLibraryItem>();
        public void AddLibraryItem(AbstractLibraryItem item) { }
        public Dictionary<int, AbstractLibraryItem> GetAllAbstractLibraryItems() { return libraryCatalog; }
    }
}
