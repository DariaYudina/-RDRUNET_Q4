using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionValidation
{
    public abstract class LibraryValidation<T> : IAbstractValidation<T> 
                          where T : AbstractLibraryItem
    {
    }
}
