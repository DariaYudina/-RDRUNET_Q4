using System.Configuration;
using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.AbstractDAL.INewspaper;
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

        public static ICommonLogic CommonLogic => new CommonLogic(CommonDao);

        public static ICommonDao CommonDao { get; }

        public static ICommonValidation CommonValidation => new CommonValidation();

        public static IPatentLogic PatentLogic => new PatentLogic(PatentDao, PatentValidation);

        public static IPatentDao PatentDao { get; }

        public static IPatentValidation PatentValidation => new PatentValidation(CommonValidation);

        public static INewspaperValidation NewspaperValidation => new NewspaperValidation(CommonValidation);

        public static INewspaperLogic NewspaperLogic => new NewspaperLogic(NewspaperDao, NewspaperValidation);

        public static INewspaperDao NewspaperDao { get; }

        public static IIssueValidation IssueValidation => new IssueValidation(CommonValidation, NewspaperValidation);

        public static IIssueLogic IssueLogic => new IssueLogic(IssueDao, IssueValidation);

        public static IIssueDao IssueDao { get; }

        static DependencyResolver()
        {
            SqlConnectionConfig sqlConnectionConfig = new SqlConnectionConfig(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            CommonDao = new CommonDBDao(sqlConnectionConfig);
            BookDao = new BookDBDao(sqlConnectionConfig);
            PatentDao = new PatentDBDao(sqlConnectionConfig);
            NewspaperDao = new NewspaperDBDao(sqlConnectionConfig);
            IssueDao = new IssueDBDao(sqlConnectionConfig);
        }
    }
}
