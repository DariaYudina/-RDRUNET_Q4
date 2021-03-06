using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IPatentValidation
    {
        List<ValidationException> ValidationResult { get; set; }
        bool IsValid { get; set; }
        IPatentValidation CheckCountry(Patent patent);
        IPatentValidation CheckRegistrationNumber(Patent patent);
        IPatentValidation CheckApplicationDate(Patent patent);
        IPatentValidation CheckPublicationDate(Patent patent);
    }
}
