namespace OpenAI_Integration.Services
{
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    using OpenAI_Integration.Interfaces;
    using OpenAI_Integration.Models.ChatCompletion;
	using OpenAI_Integration.Models.ImageGeneration;
	using OpenAI_Integration.Models.TextEdit;

	public class ApiRequestService : IApiRequestService
	{
        private readonly IConfiguration configuration;

        public ApiRequestService(IConfiguration configuration)
        {
            this.configuration = configuration;
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

			var uri = new Uri("https://api.openai.com/v1/chat/completions");
			return await this.Post(JsonConvert.SerializeObject(requestContent), uri);
		}

		public async Task<string> Get(string input, string instruction)
		{
			var requestContent = new TextRequestContent()
			{
				Model = "code-davinci-edit-001",
				Input = input,
				Instruction = instruction
			};

			var uri = new Uri("https://api.openai.com/v1/edits");
			return await this.Post(JsonConvert.SerializeObject(requestContent), uri);
		}

		public async Task<string> Get(string prompt, int numberOfImages, string imageSize)
		{
			var requestContent = new ImageRequestContent()
			{
				Prompt = prompt,
				NumberImages = numberOfImages,
				Size = imageSize
			};

			var uri = new Uri("https://api.openai.com/v1/images/generations");
			return await this.Post(JsonConvert.SerializeObject(requestContent), uri);
		}

		public async Task<string> Post(string requestContent, Uri uri) 
		{
			string key = $"{configuration.GetValue<string>("key")}";
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = uri,
				Headers =
				{
					{ "Authorization", $"Bearer {key}" }
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
