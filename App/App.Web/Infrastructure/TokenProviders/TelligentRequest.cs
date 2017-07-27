using System.Net.Http;

namespace App.Web.Infrastructure.TokenProviders
{
	public class TokenRequest
	{
		private string contentType = "application/json";
		private HttpMethod httpMethod;
		private string requestPathAndQuery;

		public TokenRequest(HttpMethod httpMethod)
		{
			this.httpMethod = httpMethod;
		}

		public TokenRequest(HttpMethod httpMethod, string requestPathAndQuery)
				: this(httpMethod)
			{
			this.requestPathAndQuery = requestPathAndQuery;
		}

		public TokenRequest(HttpMethod httpMethod, string requestPathAndQuery, string postData)
				: this(httpMethod, requestPathAndQuery)
			{
			this.PostData = postData;
		}

		public HttpMethod HttpMethod
		{
			get
			{
				if (this.httpMethod == null)
				{
					this.httpMethod = HttpMethod.Get;
				}

				return this.httpMethod;
			}
		}

		public string RequestPathAndQuery
		{
			get
			{
				return this.requestPathAndQuery;
			}
		}

		public string ContentType
		{
			get
			{
				return this.contentType;
			}

			set
			{
				this.contentType = value;
			}
		}

		public string PostData { get; private set; }
	}
}