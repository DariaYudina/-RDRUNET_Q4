using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System.Collections.Generic;

namespace AbstractValidation
{
    public interface IPatentValidation
    {
        ValidationObject ValidationObject { get; set; }

        IPatentValidation CheckCountry(Patent patent);

        IPatentValidation CheckRegistrationNumber(Patent patent);

        IPatentValidation CheckApplicationDate(Patent patent);

        IPatentValidation CheckPublicationDate(Patent patent);

        IPatentValidation CheckAuthors(Patent patent);

        IPatentValidation CheckByCommonValidation(Patent patent);
    }
}
