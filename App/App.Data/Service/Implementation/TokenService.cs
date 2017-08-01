using App.Data.Service.Abstraction;
using App.Data.Utilities;
using App.Models.Employees;
using System.Net.Http;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Web;

namespace App.Data.Service.Implementation
{
	public class TokenService : ITokenService
	{
		private IUoWData data;
		private IRequestExecutor executor;

		public TokenService(IUoWData data, IRequestExecutor executor)
		{
			this.data = data;
			this.executor = executor;
		}

		public string AquireToken()
		{
			string currentDomain = this.GetCurrentDomain();
			string subscribeEndpoint = "/api/clients/subscribe?date=2016-03-10&callback=" + currentDomain + "/api/reports";
			TokenRequest request = new TokenRequest(HttpMethod.Get, subscribeEndpoint);
			return this.executor.ExecuteRequest(request);
		}

		private string GetCurrentDomain()
		{
			return HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host +
			(HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
		}

		public bool SaveToken(string tokenJson)
		{
			ReportingToken incomingToken = JsonConvert.DeserializeObject<ReportingToken>(tokenJson);
			ReportingToken dbToken = this.data.Tokens.All().Where(t => t.Token == incomingToken.Token).FirstOrDefault();
			if (dbToken == null)
			{
				ReportingToken newToken = new ReportingToken
				{
					Expires = incomingToken.Expires,
					Token = incomingToken.Token
				};

				this.data.Tokens.Add(newToken);
				this.data.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public ReportingToken GetToken()
		{
			return this.data.Tokens.All().FirstOrDefault();
		}

		public bool TokenExists(Guid token)
		{
			return this.data.Tokens.All().Any(t => t.Token == token);
		}
	}
}
