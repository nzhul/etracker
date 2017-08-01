using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class EmployeesController : Controller
	{
		public ActionResult Details(int id)
		{
			ViewBag.EmployeeId = id;
			return this.View();
		}
	}
}