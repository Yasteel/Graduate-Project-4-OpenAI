using OpenAI_Integration.Models;

namespace OpenAI_Integration.Interfaces
{
    public interface ILocalStorageService
    {
        string Get();

        void Set(List<Message> value);

        void Add(string message);

        void Remove();
    }
}
