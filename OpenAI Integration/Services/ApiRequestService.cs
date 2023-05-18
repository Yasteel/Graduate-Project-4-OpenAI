namespace OpenAI_Integration.Services
{
	using System.Net.Http.Headers;

	using Newtonsoft.Json;

	using OpenAI_Integration.Interfaces;
	using OpenAI_Integration.Models;

	public class ApiRequestService : IApiRequestService
	{
		public void GetChat()
		{

		}


		public async Task<string> Get(string message)
		{
			var requestContent = new ChatRequestContent()
			{
				model = "gpt-3.5-turbo",
				messages = new List<Message>()
				{
					new Message()
					{
						role = "user",
						content = message,
					}
				}
			};

			return await this.Post(JsonConvert.SerializeObject(requestContent));

		}

		public async Task<string> Post(string requestContent) 
		{
			const string API_KEY = "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";

			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
				Headers =
				{
					{ "Authorization", $"Bearer {API_KEY}" }
				},

                Content = new StringContent(requestContent)
				{
					Headers =
					{
						ContentType = new MediaTypeHeaderValue("application/json")
					}
				}
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return body;
			}

		}
	}
}
