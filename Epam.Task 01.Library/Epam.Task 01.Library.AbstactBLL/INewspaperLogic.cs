using AbstractValidation;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

// кодестайл 

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface INewspaperLogic
    {
        bool AddNewspaper(List<ValidationObject> validationResult, Issue newspaper);
        IEnumerable<Issue> GetNewspaperItems();
    }
}
