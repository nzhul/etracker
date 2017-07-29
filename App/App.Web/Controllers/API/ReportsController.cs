using App.Data;
using App.Data.Service.Abstraction;
using App.Data.Service.Implementation;
using App.Models.InputModels;
using App.Web.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace App.Web.Controllers.API
{
	public class ReportsController : ApiController
	{
		private IReportsService reportsService;

		public ReportsController(IReportsService reportsService)
		{
			this.reportsService = reportsService;
		}

		public ReportsController()
			: this(new ReportsService(new UoWData()))
		{
		}

		[HttpPost]
		[AuthorizeReportRequest]
		public IHttpActionResult Post(ICollection<ReportInputData> models)
		{
			if (models == null || models.Count == 0)
			{
				return this.Json(new { status = "fail", message = "Empty reports collection" });
			}

			this.reportsService.CreateMultipleReports(models);

			return this.Json(new { status = "success", message = "Reports where saved." });
		}
	}
}