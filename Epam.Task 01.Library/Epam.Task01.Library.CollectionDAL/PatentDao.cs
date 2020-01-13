﻿using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.CollectionDAL
{
    public class PatentDao : IPatentDao
    {
        public void AddLibraryItem(Patent item)
        {
            MemoryStorage.AddLibraryItem(item);
        }
        public IEnumerable<Patent> GetPatentItems()
        {
            return MemoryStorage.GetLibraryItemByType<Patent>();
        }
    }
}