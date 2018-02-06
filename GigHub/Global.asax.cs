using AutoMapper;
using GigHub.App_Start;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common.WebHost;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GigHub
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
        }
    }
}
