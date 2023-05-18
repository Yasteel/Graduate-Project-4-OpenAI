namespace OpenAI_Integration.Services
{
    using OpenAI_Integration.Interfaces;
    using OpenAI_Integration.Models;

    public class MessageService : IMessageService
    {
        private List<Message> messages;

        public MessageService()
        {
            this.messages = new();
        }

        public List<Message> Get()
        {
            return this.messages;
        }

        public void Set(List<Message> messages)
        {
            this.messages.Clear();
            this.messages = messages;
        }

        public void Add(Message message)
        {
            this.messages.Add(message);
        }

        public void Remove()
        {
            this.messages.Clear();
        }
    }
}
