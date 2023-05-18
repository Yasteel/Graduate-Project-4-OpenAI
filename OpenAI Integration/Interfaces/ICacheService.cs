namespace OpenAI_Integration.Interfaces
{
    public interface ICacheService
    {
        string Get(string key);

        void Set(string key, string value);
    }
}
