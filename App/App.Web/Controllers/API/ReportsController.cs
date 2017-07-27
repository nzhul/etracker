using System.Collections.Generic;
using System.Web.Http;

namespace App.Web.Controllers.API
{
	public class ReportsController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get()
		{
			return Ok("Message");
		}

		[HttpPost]
		public IHttpActionResult Post(ICollection<object> models)
		{
			return this.Json(new { success = true });
		}
	}
}