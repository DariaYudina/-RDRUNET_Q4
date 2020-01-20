using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.CollectionDAL;

namespace Epam.Task01.Library.Common
{
    public static class DependencyResolver
    {

        private static readonly ICommonLogic _commonLogic;
        private static readonly ICommonDao _commonDao;
        private static readonly ICommonValidation _commonValidation;

        private static readonly IBookLogic _bookLogic;
        private static readonly IBookDao _bookDao;
        private static readonly IBookValidation _bookValidation;

        private static readonly IPatentLogic _patentLogic;
        private static readonly IPatentDao _patentDao;
        private static readonly IPatentValidation _patentValidation;

        private static readonly INewspaperLogic _newspaperLogic;
        private static readonly INewspaperDao _newspaperDao;
        private static readonly INewspaperValidation _newspaperValidation;
        public static IBookLogic BookLogic => _bookLogic;
        public static IBookDao BookDao => _bookDao;
        public static IBookValidation BookValidation => _bookValidation;
        public static ICommonLogic CommonLogic => _commonLogic;
        public static ICommonDao CommonDao => _commonDao;
        public static ICommonValidation CommonValidation => _commonValidation;

        public static IPatentLogic PatentLogic => _patentLogic;

        public static IPatentDao PatentDao => _patentDao;

        public static IPatentValidation PatentValidation => _patentValidation;

        public static INewspaperLogic NewspaperLogic => _newspaperLogic;

        public static INewspaperDao NewspaperDao => _newspaperDao;

        public static INewspaperValidation NewspaperValidation => _newspaperValidation;
        static DependencyResolver()
        {
            _commonDao = new CommonDao();
            _bookDao = new BookDao();
            _patentDao = new PatentDao();
            _newspaperDao = new NewspaperDao();

            _commonValidation = new CommonValidation();
            _bookValidation = new BookValidation(_commonValidation);
            _patentValidation = new PatentValidation(_commonValidation);
            _newspaperValidation = new NewspaperValidation();

            _commonLogic = new CommonLogic(_commonDao, _commonValidation);
            _bookLogic = new BookLogic(_bookDao,_bookValidation);
            _patentLogic = new PatentLogic(_patentDao, _patentValidation);
            _newspaperLogic = new NewspaperLogic(_newspaperDao, _newspaperValidation);
        }
    }
}
