using App.Data.Service.Abstraction;
using App.Models.Employees;
using System;
using System.Linq;
using System.Data.Entity;

namespace App.Data.Service.Implementation
{
	public class EmployeeService : IEmployeeService
	{
		private IUoWData data;
		private const int defaultPageSize = 3;
		private const int defaultPage = 0;

		public EmployeeService(IUoWData data)
		{
			this.data = data;
		}

		public IQueryable<Employee> GetEmployees(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			IQueryable<Employee> employees = this.data.Employees
				.All()
				.Include(t => t.Reports)
				.Include(t => t.Teams)
				.Include(t => t.Manager)
				.Include(t => t.Role);

			employees = employees.OrderByDescending(t => t.Name).Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return employees;
		}

		public int GetEmployeesCount()
		{
			return this.data.Employees.All().Count();
		}
	}
}