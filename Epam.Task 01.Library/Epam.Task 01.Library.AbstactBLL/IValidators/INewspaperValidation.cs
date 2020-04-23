using Epam.Task01.Library.Entity;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public interface INewspaperValidation
    {
        ValidationObject ValidationObject { get; set; }

        INewspaperValidation CheckNewspaperCity(Newspaper newspaper);

        INewspaperValidation CheckPublishingCompany(Newspaper newspaper);

        INewspaperValidation CheckISSN(Newspaper newspaper);

        INewspaperValidation CheckTitle(Newspaper newspaper);
    }
}
