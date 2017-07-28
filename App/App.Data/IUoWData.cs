using App.Data.Repositories;
using App.Models;
using App.Models.Employees;
using App.Models.Images;
using App.Models.Roles;
using App.Models.Teams;
using System.Data.Entity;

namespace App.Data
{
	public interface IUoWData
	{
		DbContext Context { get; }

		IRepository<ApplicationUser> Users { get; }

		IRepository<Employee> Employees { get; }

		IRepository<Report> Reports { get; }

		IRepository<Team> Teams { get; }

		IRepository<CompanyRole> Roles { get; }

		IRepository<Image> Images { get; }

		IRepository<Token> Tokens { get; }

		int SaveChanges();
	}
}