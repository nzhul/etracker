using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Employees
{
	public class Report
	{
		[Key]
		public int Id { get; set; }

		public int EmployeeId { get; set; }

		[ForeignKey("EmployeeId")]
		public Employee Employee { get; set; }

		public DateTime Date { get; set; }
	}
}