using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public abstract class AbstractLibraryItem
    {
        public int LibaryItemId { get; set; }
        public string Title { get; set; }
        public int PagesCount { get; set; }
        public string Commentary { get; set; }
        public int YearOfPublishing { get; set; }
        public AbstractLibraryItem(int libaryItemId, string title, int pagesCount, string commentary) 
        {
            LibaryItemId = libaryItemId;
            Title = title;
            PagesCount = pagesCount;
            Commentary = commentary;
        }
    }
}
