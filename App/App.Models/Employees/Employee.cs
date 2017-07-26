using App.Models.Roles;
using App.Models.Teams;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Employees
{
	public class Employee
	{
		private ICollection<Team> teams;

		public Employee()
		{
			this.teams = new HashSet<Team>();
		}

		public int Id { get; set; }

		public int Age { get; set; }

		public int? RoleId { get; set; }

		[ForeignKey("RoleId")]
		public virtual CompanyRole Role { get; set; }

		public int? ManagerId { get; set; }

		[ForeignKey("ManagerId")]
		public virtual Employee Manager { get; set; }

		public virtual ICollection<Team> Teams
		{
			get { return this.teams; }
			set { this.teams = value; }
		}
	}
}