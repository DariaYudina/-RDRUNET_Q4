﻿using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface ICommonValidation
    {
        List<ValidationObject> ValidationResult { get; set; }
        bool IsValid { get; set; }
        ICommonValidation CheckTitle(AbstractLibraryItem item);
        ICommonValidation CheckPagesCount(AbstractLibraryItem item);
        ICommonValidation CheckCommentary(AbstractLibraryItem item);
        bool CheckNumericalInRange(int number, int? timberline, int? bottomline);
    }
}
