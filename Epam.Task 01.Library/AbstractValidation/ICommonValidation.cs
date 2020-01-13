using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractValidation
{
    public interface ICommonValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set; }
        ICommonValidation CheckNullReferenceObject(AbstractLibraryItem item);
        ICommonValidation CheckTitle(AbstractLibraryItem item);
        ICommonValidation CheckPagesCount(AbstractLibraryItem item);
        ICommonValidation CheckCommentary(AbstractLibraryItem item);
        bool CheckStringIsNullorEmpty(string str);
    }
}
