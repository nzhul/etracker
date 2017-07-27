using App.Models.Roles;
using App.Models.Teams;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Employees
{
	public class Employee
	{
		private ICollection<Team> teams;
		private ICollection<Report> reports;

		public Employee()
		{
			this.teams = new HashSet<Team>();
			this.reports = new HashSet<Report>();
		}

		[Key]
		public int Id { get; set; }

		public int Age { get; set; }

		public string Email { get; set; }

		public string SurName { get; set; }

		public string Name { get; set; }

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

		public virtual ICollection<Report> Reports
		{
			get { return this.reports; }
			set { this.reports = value; }
		}
	}
}