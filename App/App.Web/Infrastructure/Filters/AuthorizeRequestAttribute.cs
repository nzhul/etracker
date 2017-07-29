namespace App.Web.Infrastructure.Filters
{
	using App.Data;
	using App.Data.Service.Abstraction;
	using App.Data.Service.Implementation;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Filters;

	public class AuthorizeReportRequestAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext context)
		{
			base.OnActionExecuting(context);

			Guid token;

			if (!this.TryGetTokenFromHeader(context, out token))
			{
				context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest);

				return;
			}

			ITokenService tokenService = new TokenService(new UoWData());

			bool tokenExists = tokenService.TokenExists(token);
			if (!tokenExists)
			{
				context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		private bool TryGetTokenFromHeader(HttpActionContext actionContext, out Guid token)
		{
			IEnumerable<string> headerValues;

			if (actionContext.Request.Headers.TryGetValues("X-Fourth-Token", out headerValues))
			{
				var header = headerValues.FirstOrDefault();

				return Guid.TryParse(header, out token);
			}

			token = Guid.Empty;

			return false;
		}
	}
}