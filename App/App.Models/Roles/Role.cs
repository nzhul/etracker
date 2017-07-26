using App.Models.Employees;
using System.Collections.Generic;

namespace App.Models.Roles
{
	public class CompanyRole
	{
		private ICollection<Employee> employees;

		public CompanyRole()
		{
			this.employees = new HashSet<Employee>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Employee> Employees
		{
			get { return this.employees; }
			set { this.employees = value; }
		}
	}
}