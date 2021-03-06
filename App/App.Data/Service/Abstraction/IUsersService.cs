﻿using App.Models;
using System.Linq;
using System.Web;
using App.Models.InputModels;

namespace App.Data.Service.Abstraction
{
	public interface IUsersService
	{
		IQueryable<ApplicationUser> GetUsers(int? page, int? pagesize);

		IQueryable<ApplicationUser> GetInactiveUsers();

		ApplicationUser GetUserById(string id);

		ApplicationUser GetApplicationAdmin();

		void UpdateUser(ApplicationUser user);

		bool UpdateUser(string id, EditClientInputModel inputModel);

		byte[] UploadProfileImage(HttpPostedFileBase uploadedImage, string userId);

		int GetUsersCount();

		void DeactivateUser(string id);

		void ActivateUser(string id);

		bool UserExists(string id);

		bool DeleteUserPhoto(string id);

		ApplicationUser GetUserByEmail(string email);
	}
}