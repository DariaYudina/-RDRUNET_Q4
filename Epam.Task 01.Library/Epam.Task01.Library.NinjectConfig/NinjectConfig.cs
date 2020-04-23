using System.Configuration;
using System.Web.Security;
using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionBLL.Validators;
using Epam.Task01.Library.DBDAL;
using Ninject;

namespace Epam.Task01.Library.NinjectConfig
{
    public static class NinjectConfig
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<ICommonLogic>()
                .To<CommonLogic>()
                .InTransientScope();
            kernel
                .Bind<ICommonDao>()
                .To<CommonDBDao>()
                .InSingletonScope();
            kernel
                .Bind<ICommonValidation>()
                .To<CommonValidation>()
                .InTransientScope();
            kernel
                .Bind<IBookLogic>()
                .To<BookLogic>()
                .InTransientScope();
            kernel
                .Bind<IBookDao>()
                .To<BookDBDao>()
                .InSingletonScope();
            kernel
                .Bind<IBookValidation>()
                .To<BookValidation>()
                .InTransientScope();
            kernel
                .Bind<IPatentLogic>()
                .To<PatentLogic>()
                .InTransientScope();
            kernel
                .Bind<IPatentDao>()
                .To<PatentDBDao>()
                .InSingletonScope();
            kernel
                .Bind<IPatentValidation>()
                .To<PatentValidation>()
                .InTransientScope();
            kernel
                .Bind<IIssueLogic>()
                .To<IssueLogic>()
                .InTransientScope();
            kernel
                .Bind<IIssueDao>()
                .To<IssueDBDao>()
                .InSingletonScope();
            kernel
                .Bind<IIssueValidation>()
                .To<IssueValidation>()
                .InTransientScope();
            kernel
                .Bind<INewspaperLogic>()
                .To<NewspaperLogic>()
                .InTransientScope();
            kernel
                .Bind<INewspaperDao>()
                .To<NewspaperDBDao>()
                .InSingletonScope();
            kernel
                .Bind<INewspaperValidation>()
                .To<NewspaperValidation>()
                .InTransientScope();
            kernel
                .Bind<IAuthorLogic>()
                .To<AuthorLogic>()
                .InTransientScope();
            kernel
                .Bind<IAuthorDao>()
                .To<AuthorDBDao>()
                .InSingletonScope();
            kernel
                .Bind<IAuthorValidation>()
                .To<AuthorValidation>()
                .InTransientScope();
            kernel
                .Bind<IUserLogic>()
                .To<UserLogic>()
                .InTransientScope();
            kernel
                .Bind<IUserDao>()
                .To<UserDBDao>()
                .InSingletonScope();
            kernel
                .Bind<IUserValidation>()
                .To<UserValidation>()
                .InTransientScope();
            kernel
                .Bind<ILoggerDao>()
                .To<LoggerDBDao>()
                .InSingletonScope();
            kernel.Bind<SqlConnectionConfig>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        }
    }
}
