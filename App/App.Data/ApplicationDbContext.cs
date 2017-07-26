using App.Data.Migrations;
using App.Models;
using App.Models.Employees;
using App.Models.Images;
using App.Models.Roles;
using App.Models.Teams;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace App.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("etrackerConnection", throwIfV1Schema: false)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public IDbSet<Image> Images { get; set; }
		public IDbSet<Employee> Employees { get; set; }
		public IDbSet<Team> Teams { get; set; }
		public IDbSet<CompanyRole> CompanyRoles { get; set; }
	}
}