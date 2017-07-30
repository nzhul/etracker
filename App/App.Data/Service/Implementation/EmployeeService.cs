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

		public IQueryable<Employee> GetEmployees(int? page, int? pagesize, SortDirection? dir, SortType? type)
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
				dir = SortDirection.Asc;
			}

			if (type == null)
			{
				type = new SortType();
				type = SortType.FirstName;
			}

			IQueryable<Employee> employees = this.data.Employees
				.All()
				.Include(t => t.Reports)
				.Include(t => t.Teams)
				.Include(t => t.Manager)
				.Include(t => t.Role);

			employees = this.ApplySorting(employees, dir, type);

			employees = employees.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return employees;
		}

		public int GetEmployeesCount()
		{
			return this.data.Employees.All().Count();
		}

		// TODO: handle code duplication that occurs with ReportsService
		private IQueryable<Employee> ApplySorting(IQueryable<Employee> employees, SortDirection? dir, SortType? type)
		{
			switch (dir.Value)
			{
				case SortDirection.Asc:
					switch (type.Value)
					{
						case SortType.FirstName:
							employees = employees.OrderBy(r => r.Name);
							break;
						case SortType.SurName:
							employees = employees.OrderBy(r => r.SurName);
							break;
						case SortType.Age:
							employees = employees.OrderBy(r => r.Age);
							break;
						case SortType.Email:
							employees = employees.OrderBy(r => r.Email);
							break;
						case SortType.Team:
							employees = employees.OrderBy(r => r.Teams.FirstOrDefault().Name);
							break;
						case SortType.Manager:
							employees = employees.OrderBy(r => r.Manager.Name);
							break;
						case SortType.Role:
							employees = employees.OrderBy(r => r.Role.Name);
							break;
						case SortType.ReportCount:
							employees = employees.OrderBy(r => r.Reports.Count);
							break;
						default:
							break;
					}
					break;
				case SortDirection.Desc:
					switch (type.Value)
					{
						case SortType.FirstName:
							employees = employees.OrderByDescending(r => r.Name);
							break;
						case SortType.SurName:
							employees = employees.OrderByDescending(r => r.SurName);
							break;
						case SortType.Age:
							employees = employees.OrderByDescending(r => r.Age);
							break;
						case SortType.Email:
							employees = employees.OrderByDescending(r => r.Email);
							break;
						case SortType.Team:
							employees = employees.OrderByDescending(r => r.Teams.FirstOrDefault().Name);
							break;
						case SortType.Manager:
							employees = employees.OrderByDescending(r => r.Manager.Name);
							break;
						case SortType.Role:
							employees = employees.OrderByDescending(r => r.Role.Name);
							break;
						case SortType.ReportCount:
							employees = employees.OrderByDescending(r => r.Reports.Count);
							break;
						default:
							break;
					}
					break;
				default:
					break;
			}

			return employees;
		}
	}
}