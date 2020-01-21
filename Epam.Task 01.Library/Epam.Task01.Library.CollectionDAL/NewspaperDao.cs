using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.CollectionDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public void AddNewspaper(Newspaper item)
        {
            MemoryStorage.AddLibraryItem(item);
        }

        public bool CheckNewspaperUniqueness(Newspaper newspaper)
        {
            var allitems = MemoryStorage.GetAllAbstractLibraryItems();
            var newspapers = allitems.OfType<Newspaper>();

            if ( newspaper.Issue.Issn != "" && newspaper.Issue.Issn != null)
            {
                foreach (Newspaper item in newspapers)
                {
                    if (item.Issue.Issn == newspaper.Issue.Issn && item.Title == newspaper.Title)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Newspaper item in newspapers)
                {
                    if (item.Title == newspaper.Title && item.DateOfPublishing == newspaper.DateOfPublishing && item.Issue.PublishingCompany == newspaper.Issue.PublishingCompany)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            return MemoryStorage.GetLibraryItemByType<Newspaper>();
        }
    }
}
