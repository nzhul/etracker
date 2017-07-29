using System;
using System.Collections.Specialized;

namespace Utilities.Paging
{
	public static class PaginDataGenerator
	{
		public static PagingData GeneratePagingData(string rawUrl, NameValueCollection queryString, int totalItemsCount, int pageSize, int linksRadius)
		{
			Uri pageUrl = new Uri(rawUrl);
			return new PagingData(totalItemsCount, pageSize, linksRadius, false, pageUrl, queryString);
		}
	}
}