using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace App.Web.Infrastructure.TokenProviders
{
	public static class TokenProvider
	{
		// TODO: get this from config
		private static readonly Uri Uri = new Uri("http://localhost:51396/api/clients/subscribe?date=2016-03-10&callback=http://dev.etracker.com/api/reports");

		public static void AquireToken()
		{
			TokenRequest request = new TokenRequest(HttpMethod.Get, "/api/clients/subscribe?date=2016-03-10&callback=http://dev.etracker.com/api/reports");
			RequestExecutor executor = new RequestExecutor();
			var response = executor.ExecuteRequest(request);
		}
	}
}