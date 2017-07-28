using App.Data.Service.Abstraction;
using App.Models.Images;

namespace App.Data.Service.Implementation
{
	public class ImagesService : IImagesService
	{
		IUoWData data;

		public ImagesService(IUoWData data)
		{
			this.data = data;
		}

		public bool DeleteImage(int id)
		{
			Image deletedImage = this.data.Images.Delete(id);
			this.data.SaveChanges();

			if (deletedImage != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Image GetImage(int id)
		{
			return this.data.Images.Find(id);
		}
	}
}