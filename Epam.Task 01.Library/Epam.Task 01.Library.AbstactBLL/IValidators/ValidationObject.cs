using AbstractValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public class ValidationObject
    {
        public bool IsValid { get => ValidationExceptions.Count == 0; }

        public List<ValidationException> ValidationExceptions { get; }

        public ValidationObject()
        {
            ValidationExceptions = new List<ValidationException>();
        }
    }
}
