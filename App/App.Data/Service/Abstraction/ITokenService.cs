using App.Models.Employees;

namespace App.Data.Service.Abstraction
{
	public interface ITokenService
	{
		string AquireToken();

		bool SaveToken(string token);

		Token GetToken();
	}
}