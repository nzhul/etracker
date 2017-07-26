namespace App.Data.Migrations
{
	using App.Data.Service.Abstraction;
	using App.Data.Service.Implementation;
	using App.Models;
	using App.Models.Configs;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;
	using System.Web;

	public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		private IConfigService configService;

		public Configuration()
			: this(new ConfigService())
		{
		}

		public Configuration(IConfigService configService)
		{
			this.configService = configService;
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			this.InitializeAdministrator(context);
			this.InitializeEmployees(context);
		}

		private void InitializeEmployees(ApplicationDbContext context)
		{
			if (!context.Employees.Any())
			{
				IList<JsonEmployee> employeesData = JsonConvert.DeserializeObject<IList<JsonEmployee>>(new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/EmployeesData/few-employees.json")).ReadToEnd());
				foreach (var employeeDataItem in employeesData)
				{

				}
			}
		}

		private void InitializeAdministrator(ApplicationDbContext context)
		{
			if (!context.Users.Any())
			{
				IdentityRole adminRole = new IdentityRole { Name = "Admin", Id = Guid.NewGuid().ToString() };
				IdentityRole userRole = new IdentityRole { Name = "User", Id = Guid.NewGuid().ToString() };
				context.Roles.Add(adminRole);
				context.Roles.Add(userRole);

				// Initialize default user
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);
				AdminConfiguration config = this.configService.GetAdminConfiguration();

				ApplicationUser admin = new ApplicationUser();
				admin.UserName = config.Email;
				admin.FirstName = config.Firstname;
				admin.LastName = config.Lastname;
				admin.Email = config.Email;
				admin.PhoneNumber = config.Phone;
				admin.ProfileImage = this.LoadImageData(HttpContext.Current.Server.MapPath("~/App_Data/AdminProfileImage/avatar.jpg"));
				admin.RegisterDate = DateTime.UtcNow;
				admin.JobTitle = "Developer";
				admin.IsActive = true;

				userManager.Create(admin, config.Password);
				admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });
				admin.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = admin.Id });
				context.SaveChanges();
			}
		}

		private byte[] LoadImageData(string filePath)
		{
			byte[] imageData = File.ReadAllBytes(filePath);

			if (imageData != null && imageData.Length > 0)
			{
				return imageData;
			}
			else
			{
				return null;
			}
		}
	}

	public class JsonEmployee
	{
		public string Name { get; set; }
		public string SurName { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public string Role { get; set; }
		public int? ManagerId { get; set; }
		public int Id { get; set; }
		public List<string> Teams { get; set; }
	}
}