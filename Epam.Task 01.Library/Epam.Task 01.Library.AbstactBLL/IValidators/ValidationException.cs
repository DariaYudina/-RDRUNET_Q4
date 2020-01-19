using System;

namespace AbstractValidation
{
    public class ValidationObject
    {
        public string Property { get; set; }
        public string Message { get; set; }
        public ValidationObject(string message, string prop) 
        {
            Property = prop;
            Message = message;
        }
    }
}
