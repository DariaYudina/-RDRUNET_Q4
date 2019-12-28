using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractValidation
{
    public interface IAbstractValidation<T>
    {
        bool IsValid { get; }
        IAbstractValidation<T> Check1(AbstractLibraryItem item);
        IAbstractValidation<T> Check2(AbstractLibraryItem item);
    }
    public class Valid<T> : IAbstractValidation<T>
    {
        public bool IsValid { get; private set; }

        public IAbstractValidation<T> Check1(AbstractLibraryItem item)
        {
            IsValid |= item.Title.Length > 10;
            return this;
        }

        public IAbstractValidation<T> Check2(AbstractLibraryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
