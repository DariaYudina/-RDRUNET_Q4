[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Epam.Task01.Library.MVC_PL.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Epam.Task01.Library.MVC_PL.App_Start.NinjectWebCommon), "Stop")]

namespace Epam.Task01.Library.MVC_PL.App_Start
{
    using System;
    using System.Web;
    using Epam.Task01.Library.MVC_PL.Models;
    using Epam.Task01.Library.NinjectConfig;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            StandardKernel kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                NinjectConfig.RegisterServices(kernel);
                kernel.Load<AutoMapperConfig>();
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}