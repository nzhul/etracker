using App.Web.App_Start;
using App.Web.Infrastructure.ControllerFactory;
using App.Web.Infrastructure.Mapping;
using App.Web.Infrastructure.TokenProviders;
using AutoMapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace App.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Mapper.Initialize(c => c.AddProfile<MappingProfile>());
			//TokenProvider.AquireToken();
		}
	}
}