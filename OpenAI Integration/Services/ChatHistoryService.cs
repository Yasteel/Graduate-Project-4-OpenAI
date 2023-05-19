namespace OpenAI_Integration.Services
{
    using OpenAI_Integration.Interfaces;
    using OpenAI_Integration.Models;

    public class ChatHistoryService : IChatHistoryService
    {
        private List<ChatHistory> chatHistory;

        public ChatHistoryService()
        {
            this.chatHistory = new();
        }

        public List<ChatHistory> Get()
        {
            return this.chatHistory;
        }

        public void Set(List<ChatHistory> chats)
        {
            this.Remove();
            this.chatHistory = chats;
        }

        public void Add(ChatHistory chat)
        {
            this.chatHistory.Add(chat);
        }

        public void Remove()
        {
            this.chatHistory.Clear();
        }


    }
}
