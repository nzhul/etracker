﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace App.Data.Utilities
{
	public class RequestExecutor : IRequestExecutor
	{
		private string rootEndpoint;
		private const string reportServiceRootEndpointConfigKey = "reportServiceRootEndpoint";

		private string RootEndpoint
		{
			get
			{
				if (string.IsNullOrEmpty(this.rootEndpoint))
				{
					this.rootEndpoint = ConfigurationManager.AppSettings[RequestExecutor.reportServiceRootEndpointConfigKey]; ;
				}

				return this.rootEndpoint;
			}
		}

		public string ExecuteRequest(TokenRequest request)
		{
			HttpClient client = new HttpClient()
			{
				BaseAddress = new Uri(this.RootEndpoint)
			};

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.ContentType));

			string requestUri = this.RootEndpoint + request.RequestPathAndQuery;

			HttpRequestMessage nativeRequest = new HttpRequestMessage(request.HttpMethod, requestUri);

			nativeRequest.Headers.Add("Accept-Client", "Fourth-Monitor");

			return this.MakeRequest(client, nativeRequest).Result;
		}

		private async Task<string> MakeRequest(HttpClient client, HttpRequestMessage request)
		{
			try
			{
				HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				}
			}
			catch (Exception exception)
			{
				// Log the exception somewhere and hide it from the end user.
			}

			return string.Empty;
		}
	}
}
