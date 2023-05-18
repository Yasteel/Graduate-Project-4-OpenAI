using Newtonsoft.Json;
using OpenAI_Integration.Interfaces;
using OpenAI_Integration.Models;

namespace OpenAI_Integration.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private List<Message> messages;

        public LocalStorageService()
        {
            this.messages = new();
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(this.messages);
        }

        public void Set(List<Message> messages)
        {
            this.messages.Clear();
            this.messages = messages;
        }

        public void Add(string message)
        {
            this.messages.Add(new Message()
            {
                role = "user",
                content = message
            });
        }

        public void Remove()
        {
            this.messages.Clear();
        }
    }
}
