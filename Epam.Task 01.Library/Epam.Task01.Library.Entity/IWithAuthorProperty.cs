﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public interface IWithAuthorProperty
    {
         List<Author> Authors { get; set; }
    }
}
