using App.Models.Employees;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Teams
{
	public class Team
	{
		private ICollection<Employee> employees;

		public Team()
		{
			this.employees = new HashSet<Employee>();
		}

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Employee> Employees
		{
			get { return this.employees; }
			set { this.employees = value; }
		}
	}
}