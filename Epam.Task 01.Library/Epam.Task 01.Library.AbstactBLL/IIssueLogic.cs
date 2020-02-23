using AbstractValidation;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// кодестайл

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IIssueLogic
    {
        IEnumerable<Newspaper> GetIssueItems();
        bool AddIssue(List<ValidationObject> validationResult, Newspaper issue);
        Newspaper GetIssueItemById(int id);
    }
}
