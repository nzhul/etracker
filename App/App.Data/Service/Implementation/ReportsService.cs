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

		public IQueryable<Report> GetReports(int? page, int? pagesize, SortDirection? dir, SortType? type)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			if (dir == null)
			{
				dir = new SortDirection();
			}

			if (type == null)
			{
				type = new SortType();
			}

			IQueryable<Report> reports = this.data.Reports
				.All()
				.Include(t => t.Employee);

			reports = this.ApplySorting(reports, dir, type);

			reports = reports.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return reports;
		}

		// TODO: Handle code duplication that occurs with EmployeeService
		private IQueryable<Report> ApplySorting(IQueryable<Report> reports, SortDirection? dir, SortType? type)
		{
			switch (dir.Value)
			{
				case SortDirection.Asc:
					switch (type.Value)
					{
						case SortType.Date:
							reports = reports.OrderBy(r => r.Date);
							break;
						case SortType.Name:
							reports = reports.OrderBy(r => r.Employee.Name);
							break;
						case SortType.Role:
							reports = reports.OrderBy(r => r.Employee.Role.Name);
							break;
						case SortType.Team:
							reports = reports.OrderBy(r => r.Employee.Teams.FirstOrDefault().Name);
							break;
						default:
							break;
					}
					break;
				case SortDirection.Desc:
					switch (type.Value)
					{
						case SortType.Date:
							reports = reports.OrderByDescending(r => r.Date);
							break;
						case SortType.Name:
							reports = reports.OrderByDescending(r => r.Employee.Name);
							break;
						case SortType.Role:
							reports = reports.OrderByDescending(r => r.Employee.Role.Name);
							break;
						case SortType.Team:
							reports = reports.OrderByDescending(r => r.Employee.Teams.FirstOrDefault().Name);
							break;
						default:
							break;
					}
					break;
				default:
					break;
			}

			return reports;
		}

		public int GetReportsCount()
		{
			return this.data.Reports.All().Count();
		}

		public void ClearAllReports()
		{
			IList<Report> allReports = this.data.Reports.All().ToList();
			foreach (var report in allReports)
			{
				this.data.Reports.Delete(report);
			}

			this.data.SaveChanges();
		}
	}
}