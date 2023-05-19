namespace OpenAI_Integration.Interfaces
{
    using OpenAI_Integration.Models.ChatCompletion;

    public interface IMessageService
    {
        List<Message> Get();

        void Set(List<Message> value);

        void Add(Message message);

        void Remove();
    }
}
