using App.Data.Service.Abstraction;
using App.Models.Employees;
using System;

namespace App.Data.Service.Implementation
{
	public class ReportsService : IReportsService
	{
		IUoWData data;

		public ReportsService(IUoWData data)
		{
			this.data = data;
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
	}
}