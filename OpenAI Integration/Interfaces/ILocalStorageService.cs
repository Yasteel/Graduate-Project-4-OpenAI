using OpenAI_Integration.Models;

namespace OpenAI_Integration.Interfaces
{
    public interface ILocalStorageService
    {
        Task<string> Get(string key);

        void Set(string key, string value);

        void Append(string key, string value);
    }
}
