using App.Data.Service.Abstraction;
using App.Data.Utilities;
using App.Models.Employees;
using System.Net.Http;
using System.Linq;
using System;

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

		public bool SaveToken(string token)
		{
			Guid tokenValue = Guid.Parse(token);
			Token dbToken = this.data.Tokens.All().Where(t => t.Value == tokenValue).FirstOrDefault();
			if (dbToken == null)
			{
				Token newToken = new Token
				{
					Expires = DateTime.UtcNow, // Provide this value
					Value = tokenValue
				};

				this.data.Tokens.Add(newToken);
				return true;
			}
			else
			{
				return false;
			}
		}

		public Token GetToken()
		{
			return this.data.Tokens.All().FirstOrDefault();
		}
	}
}
