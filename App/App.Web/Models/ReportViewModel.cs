using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
	public class ReportViewModel
	{
		public DateTime Date { get; set; }

		public string EmployeeName { get; set; }

		public string EmployeeTeams { get; set; }

		public string Role { get; set; }
	}
}