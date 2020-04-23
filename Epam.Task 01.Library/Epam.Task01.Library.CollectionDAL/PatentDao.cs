using System.Collections.Generic;
using System.Linq;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.DBDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.CollectionDAL
{
    public class PatentDao : IPatentDao
    {
        public int AddPatent(Patent item)
        {
            return MemoryStorage.AddLibraryItem(item);
        }

        public bool CheckPatentUniqueness(Patent patent)
        {
            IEnumerable<Patent> patents = MemoryStorage.GetLibraryItemByType<Patent>();
            foreach (Patent item in patents)
            {
                if (item.RegistrationNumber == patent.RegistrationNumber && item.Country == patent.Country)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Patent> GetPatents()
        {
            return MemoryStorage.GetLibraryItemByType<Patent>();
        }

        public IEnumerable<Patent> GetPatentsByAuthorId(int id)
        {
            return MemoryStorage.GetAllAbstractLibraryItems().OfType<Patent>()
                .Where(p => p.Authors.Any(item => item.Id == id));
        }
    }
}
