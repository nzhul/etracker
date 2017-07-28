using App.Data.Service.Abstraction;
using System.Net;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class HomeController : Controller
	{
		private ITokenService tokenService;
		//private IUoWData data;

		public HomeController(ITokenService tokenService)
		{
			this.tokenService = tokenService;
		}

		public ActionResult Index()
		{
			//IEnumerable<TestimonialViewModel> model = new List<TestimonialViewModel>();
			//model = this.testimonialsService.GetTestimonials(0, 4, true).ToList().Select(t => Mapper.Map(t, new TestimonialViewModel()));
			//return View(model);
			return this.View();
		}

		[HttpGet]
		public ActionResult Subscribe()
		{
			string token = this.tokenService.AquireToken();

			if (string.IsNullOrEmpty(token))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Reporting server failed to provide auth token");
			}

			this.tokenService.SaveToken(token);

			return this.Json(new { status = "success" });
		}

	}
}