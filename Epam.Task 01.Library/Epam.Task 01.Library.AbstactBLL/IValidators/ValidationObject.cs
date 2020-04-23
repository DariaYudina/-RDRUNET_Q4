using System.Collections.Generic;
using AbstractValidation;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public class ValidationObject
    {
        public bool IsValid => ValidationExceptions.Count == 0;

        public List<ValidationException> ValidationExceptions { get; }

        public ValidationObject()
        {
            ValidationExceptions = new List<ValidationException>();
        }
    }
}
