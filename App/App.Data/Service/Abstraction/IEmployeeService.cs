﻿using App.Models.Employees;
using System.Linq;

namespace App.Data.Service.Abstraction
{
	public interface IEmployeeService
	{
		IQueryable<Employee> GetEmployees(int? page, int? pagesize);

		int GetEmployeesCount();
	}
}