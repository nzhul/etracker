using System.ComponentModel.DataAnnotations;
using System.Web;

namespace App.Models.InputModels
{
	public class EditClientInputModel
	{
		public string Id { get; set; }

		[Required(ErrorMessage = " * Required!")]
		[Display(Name = "First name:")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = " * Required!")]
		[Display(Name = "Last name:")]
		public string LastName { get; set; }

		[Required(ErrorMessage = " * Required!")]
		public string Email { get; set; }

		[Display(Name = "Company:")]
		public string Company { get; set; }

		[Display(Name = "City:")]
		public string City { get; set; }

		[Display(Name = "Address:")]
		public string Address { get; set; }

		[Display(Name = "Delivery address:")]
		public string DeliveryAddress { get; set; }

		[Required(ErrorMessage = " * Required!")]
		[Display(Name = "Phone:")]
		public string Phone { get; set; }

		[Display(Name = "Invoice data:")]
		[DataType(DataType.MultilineText)]
		public string InvoiceData { get; set; }

		[Display(Name = "Job title:")]
		public string JobTitle { get; set; }

		public HttpPostedFileBase PostedNewProfilePhoto { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		public byte[] ProfileImage { get; set; }
	}
}
