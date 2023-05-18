namespace OpenAI_Integration.Interfaces
{
	public interface IApiRequestService
	{
		Task<string> Get(string message);

		//void Get(T model);
	}
}
