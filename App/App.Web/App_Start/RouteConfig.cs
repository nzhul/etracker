﻿using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.LowercaseUrls = true;

			routes.MapRoute(
				name: "pages",
				url: "{slug}",
				defaults: new { controller = "Pages", action = "Index" },
				namespaces: new string[] { "App.Web.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Reports", action = "Log", id = UrlParameter.Optional },
				namespaces: new string[] { "App.Web.Controllers" }
			);
		}
	}
}