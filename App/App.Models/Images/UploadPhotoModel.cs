﻿using System.Collections.Generic;
using System.Web;

namespace App.Models.Images
{
	public class UploadPhotoModel
	{
		public int ItemId { get; set; }

		public int CategoryId { get; set; }

		public IEnumerable<HttpPostedFileBase> Files { get; set; }
	}
}