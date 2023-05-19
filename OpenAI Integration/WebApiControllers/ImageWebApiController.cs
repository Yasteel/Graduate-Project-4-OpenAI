using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI_Integration.Interfaces;
using OpenAI_Integration.Models.ChatCompletion;
using OpenAI_Integration.Models.ImageGeneration;
using OpenAI_Integration.Models.TextEdit;
using static System.Net.Mime.MediaTypeNames;

namespace OpenAI_Integration.WebApiControllers
{
	public class ImageWebApiController : Controller
	{
		private readonly IApiRequestService apiRequestService;
		private readonly ICacheService cacheService;
		private readonly IMessageService messageService;
		private string cacheKey = "CurrentImage";

		public ImageWebApiController
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
			var cachedData = this.cacheService.Get(this.cacheKey);

			if (cachedData is null)
			{
				return DataSourceLoader.Load(new List<Message>(), loadOptions);
			}

			var messages = this.messageService.Get();

			return DataSourceLoader.Load(messages, loadOptions);
		}

		public async Task<object> SendRequest(string? prompt, int numberOfImages, string? imageSize)
		{
			var cacheData = this.cacheService.Get(this.cacheKey);

			if (cacheData is null)
			{
				var cacheValue = $"image-generator;{DateTime.Now.ToString()};{prompt}-{numberOfImages}-{imageSize}";
				this.cacheService.Set(this.cacheKey, cacheValue);
			}

			//this.messageService.Add(new()
			//{
			//	role = "User",
			//	content = $"{prompt}-{numberOfImages}-{imageSize}",
			//});

			var response = JsonConvert.DeserializeObject<ImageResponseContent>(await this.apiRequestService.Get(prompt!, numberOfImages, imageSize));

			if (response == null)
			{
				return this.NotFound();
			}

			if (response.Data!.Any())
			{
				var responseString = string.Empty;
				foreach (var url in response.Data!)
				{
					responseString += $"{url.Path} ";
					this.messageService.Add(new()
					{
						role = "assistant",
						content = url.Path,
					});
				}

				return responseString;
			}

			return this.NotFound()!;

		}
	}
}
