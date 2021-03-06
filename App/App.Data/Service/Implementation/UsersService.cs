﻿using App.Data.Service.Abstraction;
using App.Data.Utilities;
using App.Models;
using App.Models.Configs;
using App.Models.InputModels;
using AutoMapper;
using System.Linq;
using System.Web;

namespace App.Data.Service.Implementation
{
	public class UsersService : IUsersService
	{
		private readonly IUoWData data;
		private IConfigService configService;
		private const int defaultPageSize = 10;
		private const int defaultPage = 0;

		public UsersService(IUoWData data, IConfigService configService)
		{
			this.data = data;
			this.configService = configService;
		}

		public IQueryable<ApplicationUser> GetUsers(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			IQueryable<ApplicationUser> users = this.data.Users.All().Where(u => u.IsActive == true).OrderByDescending(u => u.RegisterDate);
			users = users.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return users;
		}

		public IQueryable<ApplicationUser> GetInactiveUsers()
		{
			return this.data.Users.All().Where(u => u.IsActive == false).OrderByDescending(u => u.Email);
		}

		public ApplicationUser GetUserById(string id)
		{
			return this.data.Users.Find(id);
		}

		public void UpdateUser(ApplicationUser user)
		{
			ApplicationUser dbUser = this.data.Users.All().Single(u => u.Email == user.Email);

			dbUser.FirstName = user.FirstName;
			dbUser.LastName = user.LastName;
			dbUser.Company = user.Company;
			dbUser.PhoneNumber = user.PhoneNumber;
			dbUser.JobTitle = user.JobTitle;

			if (user.ProfileImage != null && user.ProfileImage.Length > 0)
			{
				dbUser.ProfileImage = user.ProfileImage;
			}

			this.data.SaveChanges();
		}

		public byte[] UploadProfileImage(HttpPostedFileBase uploadedImage, string userId)
		{
			ApplicationUser user = this.GetUserById(userId);
			user.ProfileImage = ImageUtilities.CropImage(uploadedImage, "width=150&height=150&crop=auto&format=jpg");

			this.UpdateUser(user);

			return user.ProfileImage;
		}

		public int GetUsersCount()
		{
			return this.data.Users.All().Where(u => u.IsActive).Count();
		}

		public void DeactivateUser(string id)
		{
			ApplicationUser dbUser = this.data.Users.Find(id);

			if (dbUser != null)
			{
				dbUser.IsActive = false;
				this.data.SaveChanges();
			}
		}

		public void ActivateUser(string id)
		{
			ApplicationUser dbUser = this.data.Users.Find(id);

			if (dbUser != null)
			{
				dbUser.IsActive = true;
				this.data.SaveChanges();
			}
		}

		public bool UserExists(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			else
			{
				return this.data.Users.All().Any(u => u.Id == id);
			}
		}

		public bool UpdateUser(string id, EditClientInputModel inputModel)
		{
			ApplicationUser dbUser = this.data.Users.Find(id);
			if (dbUser != null)
			{
				dbUser = Mapper.Map(inputModel, dbUser);

				if (inputModel.PostedNewProfilePhoto != null)
				{
					dbUser.ProfileImage = ImageUtilities.CropImage(inputModel.PostedNewProfilePhoto, "width=150&height=150&crop=auto&format=jpg");
				}

				this.data.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteUserPhoto(string id)
		{
			ApplicationUser dbUser = this.GetUserById(id);

			if (dbUser != null)
			{
				dbUser.ProfileImage = null;
				this.data.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}

		public ApplicationUser GetApplicationAdmin()
		{
			AdminConfiguration adminConfig = this.configService.GetAdminConfiguration();
			ApplicationUser theAdmin = null;
			if (adminConfig != null)
			{
				theAdmin = this.data.Users.All().Where(u => u.Email == adminConfig.Email).Single();
			}

			return theAdmin;
		}

		public ApplicationUser GetUserByEmail(string email)
		{
			return this.data.Users.All().Where(u => u.Email == email).Single();
		}
	}
}