using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Employees
{
	public class Token
	{
		[Key]
		public int Id { get; set; }

		public Guid Value { get; set; }

		public DateTime Expires { get; set; }
	}
}