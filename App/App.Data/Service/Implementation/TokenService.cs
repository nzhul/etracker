using App.Data.Service.Abstraction;
using App.Data.Utilities;
using App.Models.Employees;
using System.Net.Http;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace App.Data.Service.Implementation
{
	public class TokenService : ITokenService
	{
		private IUoWData data;

		public TokenService(IUoWData data)
		{
			this.data = data;
		}

		public string AquireToken()
		{
			TokenRequest request = new TokenRequest(HttpMethod.Get, "/api/clients/subscribe?date=2016-03-10&callback=http://dev.etracker.com/api/reports");
			RequestExecutor executor = new RequestExecutor();
			return executor.ExecuteRequest(request);
		}

		public bool SaveToken(string tokenJson)
		{
			ReportingToken incomingToken = JsonConvert.DeserializeObject<ReportingToken>(tokenJson);
			ReportingToken dbToken = this.data.Tokens.All().Where(t => t.Token == incomingToken.Token).FirstOrDefault();
			if (dbToken == null)
			{
				ReportingToken newToken = new ReportingToken
				{
					Expires = incomingToken.Expires, // Provide this value
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
