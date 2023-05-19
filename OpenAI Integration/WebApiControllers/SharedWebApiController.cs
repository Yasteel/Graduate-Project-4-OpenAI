namespace OpenAI_Integration.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;
    
    using Newtonsoft.Json;
    
    using OpenAI_Integration.Models.ChatCompletion;
    using OpenAI_Integration.Models;
    using OpenAI_Integration.Interfaces;

    public class SharedWebApiController : Controller
    {
        private readonly ICacheService cacheService;
        private readonly IChatHistoryService chatHistoryService;
        private readonly IMessageService messageService;

        public SharedWebApiController
        (
            ICacheService cacheService,
            IChatHistoryService chatHistoryService,
            IMessageService messageService
        )
        {
            this.cacheService = cacheService;
            this.chatHistoryService = chatHistoryService;
            this.messageService = messageService;
        }

        public string GetMessages(string cacheKey)
        {
            var cacheValue = this.cacheService?.Get(cacheKey);
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

        public object GetSavedChat(string chat, string key, string cacheKey)
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
