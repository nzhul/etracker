using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class HomeController : Controller
	{
		//private ITestimonialsService testimonialsService;
		//private IUoWData data;

		//public HomeController(IUoWData data, ITestimonialsService testimonialsService)
		//{
		//	this.data = data;
		//	this.testimonialsService = testimonialsService;
		//}

		public ActionResult Index()
		{
			//IEnumerable<TestimonialViewModel> model = new List<TestimonialViewModel>();
			//model = this.testimonialsService.GetTestimonials(0, 4, true).ToList().Select(t => Mapper.Map(t, new TestimonialViewModel()));
			//return View(model);
			return this.View();
		}
	}
}