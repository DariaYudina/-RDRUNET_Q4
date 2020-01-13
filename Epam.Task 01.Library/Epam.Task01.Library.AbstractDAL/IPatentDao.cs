﻿using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IPatentDao
    {
        void AddLibraryItem(Patent item);
        IEnumerable<Patent> GetPatentItems();
    }
}