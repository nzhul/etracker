namespace App.Data.Utilities
{
	public interface IRequestExecutor
	{
		string ExecuteRequest(TokenRequest request);
	}
}