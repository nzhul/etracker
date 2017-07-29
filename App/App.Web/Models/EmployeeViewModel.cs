namespace App.Web.Models
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public int Age { get; set; }

		public string Email { get; set; }

		public string Teams { get; set; }

		public int ManagerId { get; set; }

		public string ManagerName { get; set; }

		public int RoleId { get; set; }

		public string RoleName { get; set; }

		public int ReportsCount { get; set; }
	}
}