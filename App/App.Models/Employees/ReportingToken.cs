using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Employees
{
	public class ReportingToken
	{
		[Key]
		public int Id { get; set; }

		public Guid Token { get; set; }

		public DateTime Expires { get; set; }
	}
}