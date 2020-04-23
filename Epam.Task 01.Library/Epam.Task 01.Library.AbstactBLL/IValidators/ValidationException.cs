namespace AbstractValidation
{
    public class ValidationException
    {
        public string Property { get; set; }

        public string Message { get; set; }

        public ValidationException(string message, string prop)
        {
            Property = prop;
            Message = message;
        }
    }
}
