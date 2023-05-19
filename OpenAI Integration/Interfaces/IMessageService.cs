using OpenAI_Integration.Models.ChatCompletion;

namespace OpenAI_Integration.Interfaces
{
    public interface IMessageService
    {
        List<Message> Get();

        void Set(List<Message> value);

        void Add(Message message);

        void Remove();
    }
}
