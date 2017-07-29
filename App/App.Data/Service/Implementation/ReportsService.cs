using App.Data.Service.Abstraction;
using App.Models.Employees;
using App.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace App.Data.Service.Implementation
{
	public class ReportsService : IReportsService
	{
		IUoWData data;
		private const int defaultPageSize = 3;
		private const int defaultPage = 0;

		public ReportsService(IUoWData data)
		{
			this.data = data;
		}

		public void CreateMultipleReports(ICollection<ReportInputData> inputData)
		{
			foreach (var report in inputData)
			{
				Employee dbEmployee = this.data.Employees.Find(report.EmployeeId);
				if (dbEmployee != null)
				{
					Report newReport = new Report
					{
						Date = report.When
					};
					dbEmployee.Reports.Add(newReport);
				}
			}

			this.data.SaveChanges();
		}

		public Report CreateReport(int employeeId, DateTime when)
		{
			Employee dbEmployee = this.data.Employees.Find(employeeId);
			if (dbEmployee != null)
			{
				Report newReport = new Report
				{
					Date = DateTime.UtcNow,
					Employee = dbEmployee,
				};

				this.data.Reports.Add(newReport);
				this.data.SaveChanges();

				return newReport;
			}

			return null;
		}

		public IQueryable<Report> GetReports(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			IQueryable<Report> reports = this.data.Reports
				.All()
				.Include(t => t.Employee);

			reports = reports.OrderByDescending(t => t.Date).Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return reports;
		}

		public int GetReportsCount()
		{
			return this.data.Reports.All().Count();
		}
	}
}