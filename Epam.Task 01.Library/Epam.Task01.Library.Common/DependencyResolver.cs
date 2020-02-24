using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionBLL.Validators;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.DBDAL;

namespace Epam.Task01.Library.Common
{
    public static class DependencyResolver
    {
        public static IBookLogic BookLogic => new BookLogic(BookDao, BookValidation);

        public static IBookDao BookDao { get; }

        public static IBookValidation BookValidation => new BookValidation(CommonValidation);

        public static ICommonLogic CommonLogic => new CommonLogic(CommonDao, CommonValidation);

        public static ICommonDao CommonDao { get; }

        public static ICommonValidation CommonValidation => new CommonValidation();

        public static IPatentLogic PatentLogic => new PatentLogic(PatentDao, PatentValidation);

        public static IPatentDao PatentDao { get; }

        public static IPatentValidation PatentValidation => new PatentValidation(CommonValidation);

        public static IIssueLogic NewspaperLogic => new IssueLogic(NewspaperDao, NewspaperValidation);

        public static IIssueDao NewspaperDao { get; }

        public static IIssueValidation NewspaperValidation => new NewspaperValidation(CommonValidation, IssueValidation);

        public static INewspaperLogic IssueLogic => new NewspaperLogic(IssueDao, IssueValidation);

        public static INewspaperDao IssueDao { get; }

        public static INewspaperValidation IssueValidation => new IssueValidation(CommonValidation);

        static DependencyResolver()
        {
            CommonDao = new CommonDBDao();
            BookDao = new BookDBDao();
            PatentDao = new PatentDBDao();
            NewspaperDao = new IssueDBDao();
            IssueDao = new NewspaperDBDao();
        }
    }
}
