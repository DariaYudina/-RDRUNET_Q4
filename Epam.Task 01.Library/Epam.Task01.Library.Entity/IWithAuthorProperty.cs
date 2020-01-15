using System.Collections.Generic;

namespace Epam.Task01.Library.Entity
{
    public interface IWithAuthorProperty
    {
        List<Author> Authors { get; set; }
    }
}
