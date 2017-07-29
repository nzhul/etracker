using System;
using App.Models.Employees;

namespace App.Data.Service.Abstraction
{
	public interface ITokenService
	{
		string AquireToken();

		bool SaveToken(string tokenJson);

		ReportingToken GetToken();

		bool TokenExists(Guid token);
	}
}