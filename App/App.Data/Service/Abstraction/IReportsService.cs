using App.Models.Employees;
using System;

namespace App.Data.Service.Abstraction
{
	public interface IReportsService
	{
		Report CreateReport(int employeeId, DateTime when);
	}
}