namespace OpenAI_Integration.Interfaces
{
    using OpenAI_Integration.Models;

    public interface IChatHistoryService
    {
        List<ChatHistory> Get();

        void Set(List<ChatHistory> chats);

        void Add(ChatHistory chat);

        void Remove();
    }
}
