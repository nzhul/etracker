using App.Data;
using App.Data.Service.Abstraction;
using App.Data.Service.Implementation;
using App.Models.InputModels;
using App.Web.Infrastructure.Filters;
using System.Collections.Generic;
using System.Web.Http;
using System;
using Microsoft.AspNet.SignalR;
using App.Web.Notifications;

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

			this.SendClientNotification(models.Count, DateTime.UtcNow.ToString());

			return this.Json(new { status = "success", message = "Reports where saved." });
		}

		private void SendClientNotification(int reportsCount, string dateTimeString)
		{
			var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
			notificationHub.Clients.All.notify("{\"message\": \"new report\",\"count\": " + reportsCount + ", \"date\" : \"" + dateTimeString + "\"}");
		}
	}
}