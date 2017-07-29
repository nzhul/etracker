using App.Models.Employees;
using App.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Data.Service.Abstraction
{
	public interface IReportsService
	{
		Report CreateReport(int employeeId, DateTime when);

		void CreateMultipleReports(ICollection<ReportInputData> inputData);

		IQueryable<Report> GetReports(int? page, int? pagesize);

		int GetReportsCount();
	}
}