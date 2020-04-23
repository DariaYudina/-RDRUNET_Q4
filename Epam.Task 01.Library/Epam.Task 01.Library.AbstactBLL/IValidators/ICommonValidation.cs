using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;

namespace AbstractValidation
{
    public interface ICommonValidation
    {
        ValidationObject ValidationObject { get; set; }

        ICommonValidation CheckTitle(AbstractLibraryItem item);

        ICommonValidation CheckPagesCount(AbstractLibraryItem item);

        ICommonValidation CheckCommentary(AbstractLibraryItem item);

        bool CheckNumericalInRange(int number, int bottomline, int underline);
    }
}
