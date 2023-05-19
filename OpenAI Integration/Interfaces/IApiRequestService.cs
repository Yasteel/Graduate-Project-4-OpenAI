namespace OpenAI_Integration.Interfaces
{
	public interface IApiRequestService
	{
		Task<string> Get(string message);

		Task<string> Get(string input, string instruction);

		Task<string> Get(string prompt, int numberOfImages, string imageSize);
	}
}
