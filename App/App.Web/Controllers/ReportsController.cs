using App.Data.Service.Abstraction;
using App.Web.Models;
using App.Web.Notifications;
using AutoMapper;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utilities.Paging;

namespace App.Web.Controllers
{
	public class ReportsController : Controller
	{
		private ITokenService tokenService;
		private IReportsService reportsService;
		private IEmployeeService employeeService;
		private const int defaultPageSize = 10;
		private const int defaultLinksRadius = 2;

		public ReportsController(
			ITokenService tokenService,
			IReportsService reportsService,
			IEmployeeService employeeService)
		{
			this.tokenService = tokenService;
			this.reportsService = reportsService;
			this.employeeService = employeeService;
		}

		[System.Web.Mvc.Authorize]
		public ActionResult Log(int? page, int? pagesize)
		{
			IEnumerable<ReportViewModel> model = new List<ReportViewModel>();
			model = this.reportsService.GetReports(page - 1, pagesize ?? ReportsController.defaultPageSize).ToList().Select(t => Mapper.Map(t, new ReportViewModel()));

			int totalCount = this.reportsService.GetReportsCount();

			ViewBag.PagingData = PaginDataGenerator.GeneratePagingData(
				this.HttpContext.Request.Url.ToString(),
				this.HttpContext.Request.QueryString,
				totalCount,
				pagesize ?? ReportsController.defaultPageSize,
				ReportsController.defaultLinksRadius);

			return View(model);
		}

		[System.Web.Mvc.Authorize]
		public ActionResult Employees(int? page, int? pagesize)
		{
			IEnumerable<EmployeeViewModel> model = new List<EmployeeViewModel>();
			model = this.employeeService.GetEmployees(page - 1, pagesize ?? ReportsController.defaultPageSize).ToList().Select(t => Mapper.Map(t, new EmployeeViewModel()));

			int totalCount = this.employeeService.GetEmployeesCount();

			ViewBag.PagingData = PaginDataGenerator.GeneratePagingData(
				this.HttpContext.Request.Url.ToString(),
				this.HttpContext.Request.QueryString,
				totalCount,
				pagesize ?? ReportsController.defaultPageSize,
				ReportsController.defaultLinksRadius);

			return View(model);
		}

		[HttpGet]
		public ActionResult TrashReports()
		{
			this.reportsService.ClearAllReports();

			return RedirectToAction("Log");
		}

		[HttpPost]
		public ActionResult Subscribe()
		{
			string token = this.tokenService.AquireToken();

			if (string.IsNullOrEmpty(token))
			{
				return this.Json(new { status = "fail" });
			}

			this.tokenService.SaveToken(token);

			return this.Json(new { status = "success" });
		}
	}
}