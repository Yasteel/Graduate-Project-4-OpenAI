namespace OpenAI_Integration.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;
	using OpenAI_Integration.Interfaces;
    using OpenAI_Integration.Models.ChatCompletion;
	using OpenAI_Integration.Models.TextEdit;

	public class TextWebApiController : Controller
    {
        private readonly IApiRequestService apiRequestService;
        private readonly ICacheService cacheService;
        private readonly IMessageService messageService;
        private string cacheKey = "CurrentText";
        public TextWebApiController
        (
            IApiRequestService apiRequestService,
            ICacheService cacheService,
            IMessageService messageService
        )
        {
            this.apiRequestService = apiRequestService;
            this.cacheService = cacheService;
            this.messageService = messageService;
        }

        public object Get(DataSourceLoadOptions loadOptions)
        {
            //var cachedData = this.cacheService.Get(this.cacheKey);

            //if( cachedData is null)
            //{
            //    return DataSourceLoader.Load(new List<Message>(), loadOptions);
            //}

            var messages = this.messageService.Get();

            return DataSourceLoader.Load(messages, loadOptions);
        }


        public async Task<object> SendRequest(string text, string instruction)
        {
			var cacheData = this.cacheService.Get(this.cacheKey);

			if (cacheData is null)
			{
				var cacheValue = $"text-edit;{DateTime.Now.ToString()};{text}";
				this.cacheService.Set(this.cacheKey, cacheValue);
			}

			this.messageService.Add(new()
			{
				role = "User",
				content = $"{text}\n\n{instruction}",
			});

			var response = JsonConvert.DeserializeObject<TextResponseContent>(await this.apiRequestService.Get(text, instruction));

			if (response == null)
			{
				return this.NotFound();
			}

			if (response.Choices!.Any())
			{
				var responseString = string.Empty;
				foreach (var choice in response.Choices!)
				{
                    responseString += $"{choice.Text} ";
                    this.messageService.Add(new()
                    {
                        role = "assistant",
                        content = choice.Text,
                    });
				}

				return responseString;
			}

			return this.NotFound()!;
		}
    }
}
