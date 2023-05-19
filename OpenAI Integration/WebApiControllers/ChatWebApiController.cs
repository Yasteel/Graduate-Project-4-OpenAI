namespace OpenAI_Integration.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;
    
    using Microsoft.AspNetCore.Mvc;
    
    using Newtonsoft.Json;
    
    using OpenAI_Integration.Interfaces;
    using OpenAI_Integration.Models;
    using OpenAI_Integration.Models.ChatCompletion;

    public class ChatWebApiController : Controller
    {
        private readonly IApiRequestService apiRequestService;
        private readonly ICacheService cacheService;
        private readonly IMessageService messageService;
        private readonly IChatHistoryService chatHistoryService;
        private string cacheKey = "CurrentChat";

        public ChatWebApiController
        (
            IApiRequestService apiRequestService,
            ICacheService cacheService,
            IMessageService messageService,
            IChatHistoryService chatHistoryService
        )
        {
            this.apiRequestService = apiRequestService;
            this.cacheService = cacheService;
            this.messageService = messageService;
            this.chatHistoryService = chatHistoryService;
        }

        public object Get(DataSourceLoadOptions loadOptions)
        {
            var cacheData = this.cacheService.Get(this.cacheKey);

            if (cacheData is null)
            {
                return DataSourceLoader.Load(new List<Message>(), loadOptions);
            }

            var messages = this.messageService.Get();

            return DataSourceLoader.Load(messages, loadOptions);
        }


        public async Task<object> SendRequest(string message)
        {
            var cacheData = this.cacheService.Get(this.cacheKey);

            if (cacheData is null)
            {
                var cacheValue = $"chat;{DateTime.Now.ToString()};{message}";

                this.cacheService.Set(this.cacheKey, cacheValue);
            }

            this.messageService.Add(new()
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
                    this.messageService.Add(choice.Message!);
                }

                return responseString;
            }

            return this.NotFound()!;
        }

        public string GetMessages()
        {
            var cacheValue = this.cacheService?.Get(this.cacheKey);
            var localStorage = this.messageService.Get();

            return JsonConvert.SerializeObject(new LocalStorage()
            {
                CacheKey = cacheValue,
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

        public object GetSavedChat(string chat, string key)
        {
            var messages = JsonConvert.DeserializeObject<List<Message>>(chat);

            if (messages!.Any())
            {
                this.messageService.Set(messages!);
                this.cacheService.Set(cacheKey, key);
            }

            return this.Ok();
        }
    }
}
