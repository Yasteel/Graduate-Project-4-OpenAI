using OpenAI_Integration.Models;

namespace OpenAI_Integration.Interfaces
{
    public interface IChatHistoryService
    {
        List<ChatHistory> Get();

        void Set(List<ChatHistory> chats);

        void Add(ChatHistory chat);

        void Remove();
    }
}
