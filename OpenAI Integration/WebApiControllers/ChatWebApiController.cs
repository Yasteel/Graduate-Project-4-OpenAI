using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI_Integration.Interfaces;
using OpenAI_Integration.Models;

namespace OpenAI_Integration.WebApiControllers
{
    public class ChatWebApiController : Controller
    {
        private readonly IApiRequestService apiRequestService;
        private readonly ICacheService cacheService;
        private readonly IMessageService localStorageService;
        private readonly IChatHistoryService chatHistoryService;
        private string cacheKey = "Current";

        public ChatWebApiController
        (
            IApiRequestService apiRequestService,
            ICacheService cacheService,
            IMessageService localStorageService,
            IChatHistoryService chatHistoryService
        )
        {
            this.apiRequestService = apiRequestService;
            this.cacheService = cacheService;
            this.localStorageService = localStorageService;
            this.chatHistoryService = chatHistoryService;
        }

        public object Get(DataSourceLoadOptions loadOptions)
        {
            var cacheData = this.cacheService.Get(this.cacheKey);

            if (cacheData is null)
            {
                return DataSourceLoader.Load(new List<Message>(), loadOptions);
            }

            var localStorageData = this.localStorageService.Get();

            return DataSourceLoader.Load(localStorageData, loadOptions);
        }


        public async Task<object> SendRequest(string message)
        {
            var cacheData = this.cacheService.Get(this.cacheKey);

            if (cacheData is null)
            {
                var cacheValue = $"chat;{DateTime.Now.ToString()};{message}";

                this.cacheService.Set(this.cacheKey, cacheValue);
            }

            this.localStorageService.Add(new()
            {
                role = "User",
                content = message,
            });


            var response = JsonConvert.DeserializeObject<ChatResponseContent>(await this.apiRequestService.Get(message));

            if (response == null)
            {
                return this.NotFound();
            }

            if (response.choices!.Any())
            {
                var responseString = string.Empty;
                foreach (var choice in response.choices!)
                {
                    responseString += choice.Message!.content;
                    this.localStorageService.Add(choice.Message!);
                }

                return responseString;
            }

            return this.NotFound()!;
        }

        public void Test(string message)
        {
            this.localStorageService.Add(new Message()
            {
                role = "User",
                content = message,
            });

            //return this.Ok();
        }

        public string GetMessages()
        {
            var cacheKey = this.cacheService?.Get(this.cacheKey);
            var localStorage = this.localStorageService.Get();

            return JsonConvert.SerializeObject(new LocalStorage()
            {
                CacheKey = cacheKey,
                Messages = localStorage,
            });
        }

        public void SetChatHistory(string history)
        {
            var chatHistory = JsonConvert.DeserializeObject<List<ChatHistory>>(history);

            this.chatHistoryService.Set(chatHistory!);
        }

        public object GetChatHistory(DataSourceLoadOptions loadOptions)
        {
            var chatHistory = this.chatHistoryService.Get();

            return DataSourceLoader.Load(chatHistory, loadOptions);
        }
    }
}
