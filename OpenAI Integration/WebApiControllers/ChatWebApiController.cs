using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI_Integration.Interfaces;
using OpenAI_Integration.Models;
using System.Reflection;

namespace OpenAI_Integration.WebApiControllers
{
    public class ChatWebApiController : Controller
    {
        private readonly IApiRequestService apiRequestService;
        private readonly ICacheService cacheService;
        private readonly ILocalStorageService localStorageService;
        private string cacheKey = "Current";
        public ChatWebApiController
        (
            IApiRequestService apiRequestService,
            ICacheService cacheService,
            ILocalStorageService localStorageService
        )
        {
            this.apiRequestService = apiRequestService;
            this.cacheService = cacheService;
            this.localStorageService = localStorageService;
        }

        public object Get(DataSourceLoadOptions loadOptions)
        {
            var cacheData = this.cacheService.Get(this.cacheKey);

            if (cacheData is null)
            {
                return DataSourceLoader.Load(new List<Message>(), loadOptions);
            }

            var localStorageData = this.localStorageService.Get(this.cacheKey).ToString();

            var messages = JsonConvert.DeserializeObject<List<Message>>(localStorageData);


            return DataSourceLoader.Load(messages, loadOptions);
        }


        public async Task<object> SendRequest(string message)
        {
            var response = JsonConvert.DeserializeObject<ChatResponseContent>(await this.apiRequestService.Get(message));

            if (response == null)
            {
                return this.NotFound();
            }

            if (response.choices!.Any())
            {
                return response.choices![0].Message!.content!;
            }

            return this.NotFound()!;
        }


        public object Test(string message)
        {
            return message;
        }
    }
}
