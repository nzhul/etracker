﻿using App.Data.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace App.Web.Infrastructure.Helpers
{
	public static class AppHelpers
	{
		public static IHtmlString SortingArrow(this HtmlHelper helper, SortType sortType)
		{
			string rawUrl = HttpContext.Current.Request.Url.ToString();
			Uri newUrl = new Uri(rawUrl);

			TagBuilder tb = new TagBuilder("i");
			tb.AddCssClass("fa");

			if (newUrl.QueryParameterExist("dir"))
			{
				if (newUrl.GetQuerystringParamValue("type") == sortType.ToString())
				{
					if (newUrl.GetQuerystringParamValue("dir") == "asc")
					{
						tb.AddCssClass("fa-arrow-up");
					}
					else
					{
						tb.AddCssClass("fa-arrow-down");
					}
				}
			}

			string htmlString = tb.ToString();
			return new HtmlString(htmlString);
			// <i class="fa fa-arrow-up"></i>
		}

		public static IHtmlString SortingUrl(this HtmlHelper helper, SortType sortType)
		{
			string rawUrl = HttpContext.Current.Request.Url.ToString();
			Uri newUrl = new Uri(rawUrl);
			newUrl = newUrl.UpdateQueryParamValue("type", sortType.ToString());

			if (newUrl.QueryParameterExist("dir"))
			{
				string dirValue = newUrl.GetQuerystringParamValue("dir");
				if (dirValue == "asc")
				{
					newUrl = newUrl.UpdateQueryParamValue("dir", "desc");
				}
				else
				{
					newUrl = newUrl.UpdateQueryParamValue("dir", "asc");
				}
			}
			else
			{
				newUrl = newUrl.UpdateQueryParamValue("dir", "asc");
			}

			return new HtmlString(newUrl.ToString());
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string alt, string className, string id, string title, Dictionary<string, string> dataAttributes)
		{
			var base64 = Convert.ToBase64String(imageData);
			var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

			TagBuilder tb = new TagBuilder("img");
			tb.AddCssClass(className);
			tb.Attributes.Add("src", imgSrc);
			tb.Attributes.Add("alt", alt);
			tb.Attributes.Add("id", id);
			tb.Attributes.Add("data-toggle", "tooltip");

			foreach (var attribute in dataAttributes)
			{
				tb.Attributes.Add("data-" + attribute.Key, attribute.Value);
			}

			tb.Attributes.Add("title", title);
			string htmlString = tb.ToString(TagRenderMode.SelfClosing);

			return new HtmlString(htmlString);
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData)
		{
			return AppHelpers.ByteImage(helper, imageData, "", "", "", "", new Dictionary<string, string>());
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string alt)
		{
			return AppHelpers.ByteImage(helper, imageData, alt, "", "", "", new Dictionary<string, string>());
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string alt, string className)
		{
			return AppHelpers.ByteImage(helper, imageData, alt, className, "", "", new Dictionary<string, string>());
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string alt, string className, string id)
		{
			return AppHelpers.ByteImage(helper, imageData, alt, className, id, "", new Dictionary<string, string>());
		}

		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string alt, string className, string id, string title)
		{
			return AppHelpers.ByteImage(helper, imageData, alt, className, id, title, new Dictionary<string, string>());
		}
	}
}